using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Projectile : MonoBehaviour
{
    public float speed;
    public Vector3 flyDirection;
    public Rigidbody2D rb;
    public float damage;

    public float stopTimerAt;
    float deathTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(flyDirection * speed);

        deathTimer += Time.deltaTime;
        if (deathTimer >= stopTimerAt)
        {
            Destroy(gameObject);
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Spawner") || collision.gameObject.CompareTag("Boss"))
        {
            //Debug.Log("collision = " + collision.gameObject.transform.parent.name);
            collision.GetComponent<Enemy_BaseClass>().TakeDamage(damage, ENUM_AttackType.ranged);
            Destroy(gameObject);
        }

    }
}
