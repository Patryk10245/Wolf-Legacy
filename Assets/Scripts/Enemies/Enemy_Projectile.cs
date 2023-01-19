using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Projectile : MonoBehaviour
{
    public Vector3 flyDirection;
    public float speed;
    public float damage = 1;
    public Rigidbody2D rb;
    bool alreadyHitWall;
    public Vector3 positionAtLastFrame;

    float deathTimer;
    public float stopTimerAt = 5;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 dir = transform.position + flyDirection;
        Vector2 pos = transform.position;

        Vector2 offset = (dir - pos).normalized;
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // Update is called once per frame
    void Update()
    {
        deathTimer += Time.deltaTime;
        if(deathTimer >= stopTimerAt)
        {
            Destroy(gameObject);
        }
        positionAtLastFrame = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(alreadyHitWall == true)
        {
            Destroy(gameObject);
        }
        alreadyHitWall = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shield"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("ProjectileStopper"))
        {
            Destroy(gameObject);
        }
    }
}
