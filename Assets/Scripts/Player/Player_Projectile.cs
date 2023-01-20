using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Projectile : MonoBehaviour
{
    public Player player;
    public float speed;
    public Vector3 flyDirection;
    public Rigidbody2D rb;
    public float damage;

    public float stopTimerAt;
    float deathTimer;
    
    void Start()
    {     
        Vector2 dir = transform.position + flyDirection; 
        Vector2 pos = transform.position;

        Vector2 offset = (dir - pos).normalized;
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Update()
    {
        deathTimer += Time.deltaTime;
        if (deathTimer >= stopTimerAt)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")|| collision.gameObject.CompareTag("Boss"))
        {
            collision.GetComponent<Enemy_BaseClass>().TakeDamage(damage, ENUM_AttackType.ranged, player);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Spawner"))
        {
            collision.gameObject.GetComponent<Enemy_Spawner>().TakeDamage(damage, ENUM_AttackType.melee);
            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("ProjectileStopper"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Destroyable"))
        {
            collision.gameObject.GetComponent<Destroyable_Object>().DestroyMe();
            Destroy(gameObject);
        }

    }
}
