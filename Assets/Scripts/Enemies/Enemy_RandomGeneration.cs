using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_RandomGeneration : MonoBehaviour
{
    public Transform[] corners;
    public GameObject enemyToSpawn;
    public int minNumerOfSpawnedEnemies = 10;
    public int maxNumberOfSpawnedEnemies = 50;


    public void SpawnEnemies()
    {
        int random = Random.Range(minNumerOfSpawnedEnemies, maxNumberOfSpawnedEnemies);
        GameObject temp;

        for(int i  = 0; i < random; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(corners[0].position.x, corners[1].position.x), Random.Range(corners[0].position.y, corners[1].position.y), transform.position.z);
            temp = Instantiate(enemyToSpawn, randomPos, transform.rotation);
            temp.GetComponent<NavMeshAgent>().SetDestination(temp.transform.position + Vector3.left);
        }
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {

#if UNITY_EDITOR


        Gizmos.color = Color.red;
        Vector3 corner2 = new Vector3(corners[0].position.x, corners[1].position.y);
        Vector3 corner3 = new Vector3(corners[1].position.x, corners[0].position.y);


        Gizmos.DrawLine(corners[0].position, corner2);
        Gizmos.DrawLine(corner2, corners[1].position);
        Gizmos.DrawLine(corners[1].position, corner3);
        Gizmos.DrawLine(corner3, corners[0].position);
#endif
    }
}
