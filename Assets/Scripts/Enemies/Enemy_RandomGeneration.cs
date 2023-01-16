using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_RandomGeneration : MonoBehaviour
{
    public Transform[] corners;
    public GameObject enemyToSpawn;
    public int maxNumberOfSpawnedEnemies = 50;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            SpawnEnemies();
        }
    }

    void SpawnEnemies()
    {
        int random = Random.Range(0, maxNumberOfSpawnedEnemies);
        GameObject temp;
        Debug.Log("Enemies spawned = " + random);

        for(int i  = 0; i < random; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(corners[0].position.x, corners[1].position.x), Random.Range(corners[0].position.y, corners[1].position.y), transform.position.z);
            temp = Instantiate(enemyToSpawn, randomPos, transform.rotation);
            temp.GetComponent<NavMeshAgent>().SetDestination(temp.transform.position + Vector3.left);
        }
        Destroy(gameObject);
    }
}
