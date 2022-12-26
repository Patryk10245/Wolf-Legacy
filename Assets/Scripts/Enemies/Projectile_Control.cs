using UnityEngine;

public class Projectile_Control : MonoBehaviour
{
    public Vector3 dir;
    public float speed;
    public Vector3 initial_pos;
    public float max_Fly_Distance;
    public float damage;

    private void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);

        if (Vector3.Distance(initial_pos, transform.position) > max_Fly_Distance)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }


}
