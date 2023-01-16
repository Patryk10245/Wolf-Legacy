using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_RandomGeneration : MonoBehaviour
{
    public Transform[] corners;
    public GameObject enemyToSpawn;
    public int maxNumberOfSpawnedEnemies = 50;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    void SpawnEnemies()
    {
        int random = Random.Range(0, maxNumberOfSpawnedEnemies);

        Debug.Log("Enemies spawned = " + random);

        for(int i  = 0; i < random; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(corners[0].position.x, corners[1].position.x), Random.Range(corners[0].position.y, corners[1].position.y), transform.position.z);
            Instantiate(enemyToSpawn, randomPos, transform.rotation);
        }
        Destroy(gameObject);
    }
}
