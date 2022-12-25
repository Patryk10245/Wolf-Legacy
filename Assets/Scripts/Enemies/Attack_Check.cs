using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Check : MonoBehaviour
{
    [SerializeField] Enemy_BaseClass enemy;
    [SerializeField] bool player_In_Range;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player_In_Range = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player_In_Range = false;
        }
    }

    public void ANIM_EVENT_Attack()
    {
        if(player_In_Range)
        {
            Debug.Log("Player in Range during Attack. Chomping Health");
            Player.ins.TakeDamage(enemy.stats.Damage);
        }
    }
}
