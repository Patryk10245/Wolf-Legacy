using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    [Header("Referencje Do Zewnetrznych")]
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform enemySpawningPoint;

    [Header("Dane Wewnetrzne")]
    [SerializeField] float timer;
    [SerializeField] float TimeBetweenEnemySpawning = 5f;
    [SerializeField] float maxNumberOfEnemies = 5f;
    [SerializeField] float currentHealth;

    [Space(15)]
    [SerializeField] List<Enemy_BaseClass> listOfCreatedEnemies;


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= TimeBetweenEnemySpawning)
        {
            timer -= TimeBetweenEnemySpawning;

            // Jesli limit przeciwnikow nie jest przekroczony, spawnujemy nowego przeciwnika
            if (listOfCreatedEnemies.Count < maxNumberOfEnemies)
            {
                InstantiateEnemy();
            }
        }

    }

    void InstantiateEnemy()
    {
        // Referencja do stworzonego przeciwnika
        GameObject temp_GO;
        temp_GO = Instantiate(enemyPrefab, enemySpawningPoint.position , Quaternion.Euler(0,0,0), transform);
        temp_GO.transform.position = enemySpawningPoint.position;
        temp_GO.transform.SetParent(transform);
        // Dodanie przeciwnika do listy w spawnerze
        Enemy_BaseClass enemy = temp_GO.GetComponent<Enemy_BaseClass>();
        listOfCreatedEnemies.Add(enemy);
        enemy.is_Spawned = true;
        enemy.agent.SetDestination(enemy.transform.position + Vector3.one);
    }
    public void RemoveMe(Enemy_BaseClass enemy)
    {
        listOfCreatedEnemies.Remove(enemy);
        
    }

    public void TakeDamage(float val, ENUM_AttackType attackType)
    {
        currentHealth -= val;
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
