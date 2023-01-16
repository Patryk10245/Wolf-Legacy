using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_FireTrailObject : MonoBehaviour
{
    public Player player;
    public float expirationTime;
    float timer;

    public float damage;

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= expirationTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss"))
        {
            collision.gameObject.GetComponent<Enemy_BaseClass>().TakeDamage(damage, ENUM_AttackType.melee, player);
        }
    }
}
