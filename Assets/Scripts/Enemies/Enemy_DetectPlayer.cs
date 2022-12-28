using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_DetectPlayer : MonoBehaviour
{
    Enemy_BaseClass enemy;

    private void Start()
    {
        enemy = GetComponentInParent<Enemy_BaseClass>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            enemy.SetMoveTarget(collision.gameObject.GetComponent<Player>());
        }
    }

}
