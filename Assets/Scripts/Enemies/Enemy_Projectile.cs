using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Projectile : MonoBehaviour
{
    public Vector3 flyDirection;
    public float speed;
    public float damage;
    Rigidbody2D rb;
    bool alreadyHitWall;

    float deathTimer;
    public float stopTimerAt = 5;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        deathTimer += Time.deltaTime;
        if(deathTimer >= stopTimerAt)
        {
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        rb.AddForce(flyDirection * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision enter = " + collision.gameObject.name);
        if(alreadyHitWall == true)
        {
            Destroy(gameObject);
        }
        alreadyHitWall = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collsion = " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Shield"))
        {
            //Debug.Log("Destroyed at shield");
            Destroy(gameObject);
        }

        else if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Destroyed at player");
            collision.GetComponent<Player>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("ProjectileStopper"))
        {
            Destroy(gameObject);
        }

    }
}
