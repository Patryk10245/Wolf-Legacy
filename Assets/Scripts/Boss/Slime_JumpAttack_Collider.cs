using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_JumpAttack_Collider : MonoBehaviour
{
    public Enemy_Boss_Slime boss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            collision.GetComponent<Player>().TakeDamage(boss.bounceDamage);
        }
    }
}
