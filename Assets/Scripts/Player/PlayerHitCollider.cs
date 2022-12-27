using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitCollider : MonoBehaviour
{
    [SerializeField] Player player;
    public BoxCollider2D player_hit_collider;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("Collision");
            if (player.controller.inAttack)
            {
                //Debug.Log("is in attack");
                collision.GetComponent<Enemy_BaseClass>().TakeDamage(player.stats.damageToEnemies);
            }
        }
        
    }
}
