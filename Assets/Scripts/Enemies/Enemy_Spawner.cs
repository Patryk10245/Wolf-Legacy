using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    [Header("Refference")]
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform enemySpawningPoint;
    [SerializeField] GameObject destroyedSprite;

    [Header("Inside refference")]
    [SerializeField] float timer;
    [SerializeField] float TimeBetweenEnemySpawning = 5f;
    [SerializeField] int maxNumberOfEnemies = 5;
    [SerializeField] float currentHealth;

    [Space(15)]
    public List<Enemy_BaseClass> listOfCreatedEnemies;

    [SerializeField] List<Enemy_BaseClass> strongerMonsters;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= TimeBetweenEnemySpawning)
        {
            timer -= TimeBetweenEnemySpawning;
            if (listOfCreatedEnemies.Count < maxNumberOfEnemies)
            {
                SpawnEnemies();
            }
        }

    }

    public void RemoveMe(Enemy_BaseClass enemy)
    {
        listOfCreatedEnemies.Remove(enemy);
    }

    public void TakeDamage(float val, ENUM_AttackType attackType)
    {
        currentHealth -= val;
        if (currentHealth <= 0)
        {
            if(destroyedSprite != null)
            {
                destroyedSprite.SetActive(true);
            }
            gameObject.tag = "Untagged";
            Destroy(this);
        }
    }

    Enemy_BaseClass InstantiateEnemy()
    {
        GameObject temp_GO;
        temp_GO = Instantiate(enemyPrefab, enemySpawningPoint.position , Quaternion.Euler(0,0,0), transform);
        temp_GO.transform.position = enemySpawningPoint.position;
        temp_GO.transform.SetParent(transform);
        Enemy_BaseClass enemy = temp_GO.GetComponent<Enemy_BaseClass>();
        listOfCreatedEnemies.Add(enemy);
        enemy.is_Spawned = true;
        enemy.agent.SetDestination(enemy.transform.position + Vector3.one);

        return enemy;
    }
    

    void SpawnMoreEnemies()
    {
        int rand = Random.Range(0, maxNumberOfEnemies);
        for(int i = 0; i < rand; i++)
        {
            InstantiateEnemy();
        }

    }
    void SpawnStrongerEnemies()
    {
        Enemy_BaseClass enemy = InstantiateEnemy();

        enemy.stats.damage += 3;
        enemy.stats.maxHealth *= 2;
        enemy.stats.currentHealth *= 2;
        enemy.move_Speed += 1;
        
    }

    void SpawnEnemies()
    {
        int rand = Random.Range(0, 10);

        if(rand > -1 && rand < 2)
        {
            SpawnStrongerEnemies();
        }
        else if(rand >= 2 && rand < 5)
        {
            SpawnMoreEnemies();
        }
        else
        {
            InstantiateEnemy();
        }


        InstantiateEnemy();
    }
}
