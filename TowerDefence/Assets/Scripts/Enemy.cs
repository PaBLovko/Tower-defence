using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

 
    public int target = 0;//what point now
    public Transform exit;
    public Transform[] wayPoints;//точки к которым идти 
    public float navigation;//как часто персонаж будет обновляться(сколько кадров)
    [SerializeField]
    public Image bar;
    public float health;

    Transform enemy;
    float navigationTime = 0;//обновлять положение персонажей в простр


	void Start () {
        health = 1f;
        enemy = GetComponent<Transform>();//чтобы реализовать и считывать положение персонажа
        Manager.Instance.RegisterEnemy(this);
	}
	
	// Update is called once per frame
	void Update () {
        if (health <= 0f) {
            Manager.Instance.UnRegisterEnemy(this,true);
        }
        bar.fillAmount=health;
       // FollowEnemy();
        if (wayPoints != null) {
            navigationTime += Time.deltaTime;//чтобы двигаться к след точке
            if (navigationTime > navigation) {//если 
                if (target < wayPoints.Length)//если мы не дошли до конца
                {                           //позиция на которой сейчас  то позиция след точки       рассчет того где сейчас противник
                    enemy.position = Vector2.MoveTowards(enemy.position, wayPoints[target].position, navigationTime);
                }
                else {//если доли до конца то идем на выход
                    enemy.position = Vector2.MoveTowards(enemy.position,exit.position,navigationTime);
                }
                navigationTime =0;
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "MoveingPoint")
        {
            target++;//если дошли до цели идем к след
        }
        else if (collision.tag == "Finish") {
            Manager.Instance.UnRegisterEnemy(this,false);
        } 
    }


    public void GetDamage(float damage) {
        health -= damage;
    }


}
