using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : Loader<Manager>
{
    [SerializeField]//чтобы поля было видно в юнити
    GameObject spawnPoint;
    [SerializeField]
    GameObject[] enemies;
    [SerializeField]
    int maxEnemyOnScrean;
    [SerializeField]
    int totalEnemys;
    [SerializeField]
    int enemyType;
    [SerializeField]
    int wasEnemyOnScrean;
    [SerializeField]
    int enemiesPerSpawn;
    [SerializeField]
    GameObject pausePanel;
    bool isPaused = false;

    [SerializeField]
    int lvlnumber;

    [SerializeField]
    AudioSource error;


    [SerializeField]
    int resources;

    public int health;

    [SerializeField]
    int numberOfLifes;
    [SerializeField]
    Image[] lives;
    [SerializeField]
    Sprite fulLife;
    [SerializeField]
    Sprite emptyLife;

    public List<Enemy> EnemyList = new List<Enemy>();

    // Use this for initialization
    const float SpawnDelay = 0.5f;

    private void Update()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene(lvlnumber);
        }
        for (int i = 0; i < lives.Length; i++)
        {

            if (i < health)
            {
                lives[i].sprite = fulLife;
            }
            else
            {
                lives[i].sprite = emptyLife;
            }

            if (i < numberOfLifes)
            {
                lives[i].enabled = true;
            }
            else
            {
                lives[i].enabled = false;
            }
        }
    }

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        if (enemiesPerSpawn > 0 && EnemyList.Count < totalEnemys && wasEnemyOnScrean != totalEnemys)
        {//если спавнить нужно больше 0 и количество сущ на экране  меньше максисмума
            for (int i = 0; i < enemiesPerSpawn; i++)
            {//делаем цикл
                if (EnemyList.Count < maxEnemyOnScrean)
                {//спавним одно существо 
                    GameObject newEnemy = Instantiate(enemies[enemyType]) as GameObject;//какой тип 
                    newEnemy.transform.position = spawnPoint.transform.position;//по каким координатом
                    wasEnemyOnScrean++;
                }
            }
        }
        else
        {
            if (EnemyList.Count == 0 && wasEnemyOnScrean >= totalEnemys)
            {
                SceneManager.LoadScene(7);
            }
        }
        yield return new WaitForSeconds(SpawnDelay);//делаем задержку
        StartCoroutine(Spawn());//вызываем спавн
    }

    public void RegisterEnemy(Enemy enemy)
    {//регистр противника
        EnemyList.Add(enemy);
    }

    public void UnRegisterEnemy(Enemy enemy, bool finis)
    {//убираем противника
        EnemyList.Remove(enemy);
        if (finis == false)
        {
            health--;
            Destroy(enemy.gameObject);//убираем объект нашего противника
        }
        else
        {
            Manager.Instance.SetResources(Manager.Instance.GetResources() + enemy.GetReward());
        }
    }

    public void DestrayEnemies()
    {
        foreach (Enemy enemy in EnemyList)
        {
            Destroy(enemy.gameObject);//уничтожение противника
        }
        EnemyList.Clear();//если уничтожили всех противников очистить экран
    }

    public int GetResources()
    {
        return resources;
    }
    public void SetResources(int newResurces)
    {
        resources = newResurces;
    }
    public AudioSource GetSound()
    {
        return error;
    }

}
