using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Player_Detection : MonoBehaviour
{
    [SerializeField] Enemy_BaseClass enemy;
    [SerializeField] bool can_Detect_Player = true;
    public int amount_of_detected_players;
    

    private void Start()
    {
        if(enemy == null)
        enemy = GetComponentInParent<Enemy_BaseClass>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(can_Detect_Player == true)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                amount_of_detected_players++;
                AI_Manager.ins.ChasePlayer(enemy);
            }
        }
        
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            amount_of_detected_players--;
            AI_Manager.ins.StopChasing(enemy);
        }
    }

}
