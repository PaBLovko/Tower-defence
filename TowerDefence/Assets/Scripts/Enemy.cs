using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    Transform exit;
    [SerializeField]
    Transform[] wayPoints;
    [SerializeField]
    Canvas bar;
    [SerializeField]
    Canvas moneyPanel;
    int target = 0;
    float navigation = 0;
    Animator animator;
    public float health;
    public int reward;
    Transform enemy;
    float navigationTime = 0;
    float currentHealth;
    bool isDie = false;
    void Start()
    {
        moneyPanel = Instantiate(moneyPanel);

        bar = Instantiate(bar);
        animator = GetComponent<Animator>();
        currentHealth = health;
        enemy = GetComponent<Transform>();
        Manager.Instance.RegisterEnemy(this);
    }

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
                navigationTime += Time.deltaTime;
                if (navigationTime > navigation)
                {
                    if (target < wayPoints.Length)
                    {
                        enemy.position = Vector2.MoveTowards(enemy.position, wayPoints[target].position, navigationTime);
                    }
                    else
                    {
                        enemy.position = Vector2.MoveTowards(enemy.position, exit.position, navigationTime);
                    }
                    navigationTime = 0;
                }
            }
        }
        if (isDie)
        {
            Text tex = moneyPanel.GetComponentInChildren<Text>();
            tex.text = "" + reward;
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
            target++;
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
        canvas.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        canvas.transform.position = new Vector2(transform.position.x, transform.position.y + offset);
    }
}
