using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerControlle : MonoBehaviour {
    [SerializeField]
    float timeBetwenAttack;
    [SerializeField]
    float attackRadius;
    ProjectTile projectTile;
    Enemy targetEnemy = null;
    float attackCounter;


    private List<Enemy> getEnemiesInRange() {
        List<Enemy> enemiesInRange = new List<Enemy>();
        foreach (Enemy enemy in Manager.Instance.EnemyList) {
            if (Vector2.Distance(transform.position,enemy.transform.position)<=attackRadius) {
                enemiesInRange.Add(enemy);
            }
        }
        return enemiesInRange;
    }
    private Enemy getNearestEnemy() {
        Enemy nearestEnemy = null;
        float smollestDistense = float.PositiveInfinity;

        foreach (Enemy enemy in getEnemiesInRange()) {
            if (Vector2.Distance(transform.position, enemy.transform.position) <= smollestDistense)
            {
                smollestDistense = Vector2.Distance(transform.position, enemy.transform.position);//расстояние от башни до противника=наим раст
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }
}
