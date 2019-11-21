using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : Loader<Manager>
{
    [SerializeField]
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
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                if (EnemyList.Count < maxEnemyOnScrean)
                {
                    GameObject newEnemy = Instantiate(enemies[enemyType]) as GameObject;
                    newEnemy.transform.position = spawnPoint.transform.position;
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
        yield return new WaitForSeconds(SpawnDelay);
        StartCoroutine(Spawn());
    }

    public void RegisterEnemy(Enemy enemy)
    {
        EnemyList.Add(enemy);
    }

    public void UnRegisterEnemy(Enemy enemy, bool finis)
    {
        EnemyList.Remove(enemy);
        if (finis == false)
        {
            health--;
            Destroy(enemy.gameObject);
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
            Destroy(enemy.gameObject);
        }
        EnemyList.Clear();
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
