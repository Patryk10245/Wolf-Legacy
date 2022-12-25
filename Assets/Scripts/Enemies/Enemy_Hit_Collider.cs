using UnityEngine;

public class Enemy_Hit_Collider : MonoBehaviour
{
    public Transform attack_collider_handler;
    public Enemy_BaseClass enemy;
    public bool is_TouchingPlayer;
    public Player touched_player;

    float angle;
    Vector3 enm_pos;
    Vector3 col_pos;
    Vector3 custom_vector;
    float atan2;

    
    private void Start()
    {
        enemy = GetComponentInParent<Enemy_BaseClass>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateColiderTowardsPlayer();
    }

    private void RotateColiderTowardsPlayer()
    {
        if (enemy.GetMoveTarget() != null)
        {
            //dir = new Vector3(enemy.GetMoveTarget().position.x, enemy.GetMoveTarget().position.y - transform.position.y);
            //dir = enemy.GetMoveTarget().position - transform.position;
            //dir.z = 0;
            enm_pos = enemy.GetMoveTarget().position;
            col_pos = attack_collider_handler.position;
            atan2 = Mathf.Atan2(enm_pos.y - col_pos.y, enm_pos.x - col_pos.x) * 180 / Mathf.PI;
            angle = atan2 - 90;
            custom_vector.z = angle;
            attack_collider_handler.localRotation = Quaternion.Euler(custom_vector);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("collision name  = " + collision.gameObject.name + "\n me = " + enemy.gameObject.name);
            
            is_TouchingPlayer = true;
            touched_player = collision.gameObject.GetComponent<Player>();
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("collision name  = " + collision.gameObject.name + "\n me = " + enemy.gameObject.name);

            is_TouchingPlayer = false;
            touched_player = null;
        }
    }
}
