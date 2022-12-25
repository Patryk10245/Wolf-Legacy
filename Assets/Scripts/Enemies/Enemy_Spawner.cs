using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    [Header("Referencje Do Zewnetrznych")]
    [SerializeField] GameObject enemy_prefab;
    [SerializeField] Transform enemy_spawning_spot;

    [Header("Dane Wewnetrzne")]
    [SerializeField] float timer;
    [SerializeField] float time_between_enemy_spawning = 5f;
    [SerializeField] float max_number_of_enemies = 5f;

    [Space(15)]
    [SerializeField] List<Enemy_BaseClass> list_of_created_enemies;


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= time_between_enemy_spawning)
        {
            timer -= time_between_enemy_spawning;

            // Jesli limit przeciwnikow nie jest przekroczony, spawnujemy nowego przeciwnika
            if (list_of_created_enemies.Count < max_number_of_enemies)
            {
                InstantiateEnemy();
            }
        }

    }

    void InstantiateEnemy()
    {
        // Referencja do stworzonego przeciwnika
        GameObject temp_GO;
        temp_GO = Instantiate(enemy_prefab);
        temp_GO.transform.position = enemy_spawning_spot.position;
        temp_GO.transform.SetParent(enemy_spawning_spot);
        // Dodanie przeciwnika do listy w spawnerze
        Enemy_BaseClass enemy = temp_GO.GetComponent<Enemy_BaseClass>();
        list_of_created_enemies.Add(enemy);
        enemy.is_Spawned = true;
        enemy.agent.SetDestination(enemy.transform.position + Vector3.one);
    }
    public void RemoveMe(Enemy_BaseClass enemy)
    {
        list_of_created_enemies.Remove(enemy);
    }
}
