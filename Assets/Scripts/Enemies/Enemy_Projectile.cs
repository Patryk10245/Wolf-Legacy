using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Projectile : MonoBehaviour
{
    public Vector3 flyDirection;
    public float speed;
    public float damage;
    Rigidbody2D rb;

    float deathTimer;
    float stopTimerAt = 10;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(flyDirection * speed);

        deathTimer += Time.deltaTime;
        if(deathTimer >= stopTimerAt)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(damage);
            Destroy(gameObject);
        }
        if(collision.gameObject.CompareTag("Shield"))
        {
            Destroy(gameObject);
        }
    }
}
