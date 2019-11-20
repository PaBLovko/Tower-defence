using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    Transform exit;
    [SerializeField]
    Transform[] wayPoints;//точки к которым идти 
    [SerializeField]
    Canvas bar;
    [SerializeField]
    Canvas moneyPanel;
    int target = 0;//what point now
    float navigation = 0;//как часто персонаж будет обновляться(сколько кадров)
    Animator animator;
    public float health;
    public int reward;
    Transform enemy;
    float navigationTime = 0;//обновлять положение персонажей в простр
    float currentHealth;
    bool isDie = false;
    void Start()
    {
        moneyPanel = Instantiate(moneyPanel);
       
        bar = Instantiate(bar);
        animator = GetComponent<Animator>();
        currentHealth = health;
        enemy = GetComponent<Transform>();//чтобы реализовать и считывать положение персонажа
        Manager.Instance.RegisterEnemy(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDie)
        {
            if (currentHealth <= 0f)
            {
                animator.SetTrigger("didDie");
                Manager.Instance.UnRegisterEnemy(this, true);
                Destroy(bar.gameObject);
                isDie = true;
                navigationTime = 0;
            }
            Image image = bar.GetComponentInChildren<Image>();
            image.fillAmount = 1f / health * (health - (health - currentHealth));
            FollowEnemy(bar, 0.3f);
            if (wayPoints != null)
            {
                navigationTime += Time.deltaTime;//чтобы двигаться к след точке
                if (navigationTime > navigation)
                {//если 
                    if (target < wayPoints.Length)//если мы не дошли до конца
                    {                           //позиция на которой сейчас  то позиция след точки       рассчет того где сейчас противник
                        enemy.position = Vector2.MoveTowards(enemy.position, wayPoints[target].position, navigationTime);
                    }
                    else
                    {//если доли до конца то идем на выход
                        enemy.position = Vector2.MoveTowards(enemy.position, exit.position, navigationTime);
                    }
                    navigationTime = 0;
                }
            }
        }
        if (isDie)
        {
            Text tex = moneyPanel.GetComponentInChildren<Text>();
            tex.text =""+reward;
            navigationTime += Time.deltaTime;
            FollowEnemy(moneyPanel, 0.5f);
            if (navigationTime >= 2)
            {
                Destroy(moneyPanel.gameObject);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "MoveingPoint")
        {
            target++;//если дошли до цели идем к след
        }
        else if (collision.tag == "Finish")
        {
            Manager.Instance.UnRegisterEnemy(this, false);
        }
    }

    public int GetReward()
    {
        return reward;
    }

    public bool IsDie()
    {
        return isDie;
    }

    public void GetDamage(float damage)
    {
        currentHealth -= damage;
        animator.Play("Hurt");
    }

    public void FollowEnemy(Canvas canvas, float offset)
    {
        canvas.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);//изображение просчитывает положение относительно камеры и привяз к курсору 
        canvas.transform.position = new Vector2(transform.position.x, transform.position.y + offset);
    }
}
