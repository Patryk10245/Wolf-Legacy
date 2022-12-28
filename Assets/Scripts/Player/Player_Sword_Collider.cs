using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Sword_Collider : MonoBehaviour
{
    public Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("Take Damage");
            collision.gameObject.GetComponent<Enemy_BaseClass>().TakeDamage(player.stats.damage);
        }
    }
}