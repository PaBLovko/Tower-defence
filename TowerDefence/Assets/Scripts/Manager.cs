using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public static Manager instance = null;
    public GameObject spawnPoint;
    public GameObject[] enemies;
    public int maxEnemyOnScrean;
    public int totalEnemys;
    public int enemyType;
    public int enemiesPerSpawn;

    int enemyOnScrean = 0;

    // Use this for initialization
    const float SpawnDelay = 0.5f;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance!=this){
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start () {
        StartCoroutine(Spawn());
	}


    // Update is called once per frame
   

    IEnumerator Spawn()
    {
        if (enemiesPerSpawn > 0 && enemyOnScrean < totalEnemys) {//если спавнить нужно больше 0 и количество сущ на экране  меньше максисмума
            for (int i=0;i<enemiesPerSpawn;i++) {//делаем цикл
                if (enemyOnScrean < maxEnemyOnScrean) {//спавним одно существо 
                    GameObject newEnemy = Instantiate(enemies[enemyType]) as GameObject;//какой тип 
                    newEnemy.transform.position = spawnPoint.transform.position;//по каким координатом
                    enemyOnScrean++;//увиличиваем количество на 1
                }
            }
        } 
        yield return new WaitForSeconds(SpawnDelay);//делаем задержку
        StartCoroutine(Spawn());//вызываем спавн
    }

    public void removeEnemyFromScrean()
    {
        if (enemyOnScrean > 0) {
            enemyOnScrean--;
        }
    }

}
