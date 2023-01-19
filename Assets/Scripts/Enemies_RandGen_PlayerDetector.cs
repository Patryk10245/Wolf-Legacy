using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies_RandGen_PlayerDetector : MonoBehaviour
{
    [SerializeField] Enemy_RandomGeneration randomGeneration;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            randomGeneration.SpawnEnemies();
        }
    }
}
