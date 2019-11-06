using UnityEngine;

public class Enemy : MonoBehaviour {


    public int target = 0;//what point now
    public Transform exit;
    public Transform[] wayPoints;//точки к которым идти 
    public float navigation;//как часто персонаж будет обновляться(сколько кадров)

    Transform enemy;
    float navigationTime = 0;//обновлять положение персонажей в простр


	void Start () {
        enemy = GetComponent<Transform>();//чтобы реализовать и считывать положение персонажа
        Manager.Instance.RegisterEnemy(this);
	}
	
	// Update is called once per frame
	void Update () {
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
            Manager.Instance.UnRegisterEnemy(this);
        }
    }
}
