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
    private void FixedUpdate()
    {
        //rb.AddForce(flyDirection * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision enter = " + collision.gameObject.name);
        //Debug.Log("last pos = " + positionAtLastFrame);
        //Debug.Log("pos = " + transform.position);
        if(alreadyHitWall == true)
        {
            Destroy(gameObject);
        }
        alreadyHitWall = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("collsion = " + collision.gameObject.name);
        //Debug.Log("Last frame = " + positionAtLastFrame);
        //Debug.Log("pos = " + transform.position);

        if (collision.gameObject.CompareTag("Shield"))
        {
            //Debug.Log("Destroyed at shield");
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
