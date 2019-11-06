using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerControlle : MonoBehaviour {
    [SerializeField]
    float timeBetwenAttack;
    [SerializeField]
    float attackRadius;
    [SerializeField]
    ProjectTile projectTile;
    Enemy targetEnemy = null;
    float attackCounter;
    bool isAttacking = false;

    void Update() {
        attackCounter -= Time.deltaTime;//задержка перед выстрелом
        if (targetEnemy == null)
        {
            Enemy nearestEnemy = GetNearestEnemy();
            if (nearestEnemy != null && Vector2.Distance(transform.localPosition, nearestEnemy.transform.localPosition) <= attackRadius)
            {
                targetEnemy = nearestEnemy;
            }
        }
        else
        {
            if (attackCounter <= 0)
            {
                isAttacking = true;
                attackCounter = timeBetwenAttack;
            }
            else
            {
                isAttacking = false;
            }
            if (Vector2.Distance(transform.localPosition, targetEnemy.transform.localPosition) > attackRadius)
            {
                targetEnemy = null;
            }
        }
    }

    public void FixedUpdate()
    {
        if (isAttacking == true)
        {
            Attack();
        }
    }

    public void Attack()
    {
        isAttacking = false;
        ProjectTile newProjectTile = Instantiate(projectTile) as ProjectTile ;
        newProjectTile.transform.localPosition = transform.localPosition;//стрела появляется в башни
        if (targetEnemy == null)
        {
            Destroy(newProjectTile);
        }
        else
        {
            //move projectTile to enemy
            StartCoroutine(MoveProjectTile(newProjectTile));
        }
    }

    IEnumerator MoveProjectTile(ProjectTile projectTile)
    {
        while (GetTargetDistance(targetEnemy) > 0.20f && projectTile != null && targetEnemy != null)
        {
            var dir = targetEnemy.transform.localPosition - transform.localPosition;//приблежаемся
            var angelDirection = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;//как поворачивать снаряд чтобы выглядело что он летит 
            projectTile.transform.rotation = Quaternion.AngleAxis(angelDirection, Vector3.forward);//поворачиает
            projectTile.transform.localPosition = Vector2.MoveTowards(projectTile.transform.localPosition, targetEnemy.transform.localPosition,5f*Time.deltaTime);//летим
            yield return null;
        }

        if (projectTile != null || targetEnemy == null)
        {
            Destroy(projectTile);
        }
    }

    private float GetTargetDistance(Enemy enemy)
    {
        if (enemy == null)
        {
            enemy = GetNearestEnemy();
            if (enemy == null)
            {
                return 0f;
            }
        }
        return Mathf.Abs(Vector2.Distance(transform.localPosition, enemy.transform.localPosition));//если противник выйдет из зоны поражение снаряд пропадет 
    }

    private List<Enemy> GetEnemiesInRange() {
        List<Enemy> enemiesInRange = new List<Enemy>();
        foreach (Enemy enemy in Manager.Instance.EnemyList) {
            if (Vector2.Distance(transform.localPosition, enemy.transform.localPosition) <=attackRadius) {
                enemiesInRange.Add(enemy);
            }
        }
        return enemiesInRange;
    }
    private Enemy GetNearestEnemy() {
        Enemy nearestEnemy = null;
        float smollestDistense = float.PositiveInfinity;

        foreach (Enemy enemy in GetEnemiesInRange()) {
            if (Vector2.Distance(transform.localPosition, enemy.transform.localPosition) <= smollestDistense)
            {
                smollestDistense = Vector2.Distance(transform.localPosition, enemy.transform.localPosition);//расстояние от башни до противника=наим раст
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }
}
