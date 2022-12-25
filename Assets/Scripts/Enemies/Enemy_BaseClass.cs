using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Enemy_Stats))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Stop_Rotation))]
public abstract class Enemy_BaseClass : MonoBehaviour
{
    [Header("Scene Reference")]
    [SerializeField] protected Transform move_target;
    [SerializeField] protected Vector3 moveDirection;
    //[SerializeField] protected Player chase_target;

    [Header("Specifics")]
    [SerializeField] protected float move_Speed = 5f;
    [SerializeField] public float attack_Distance = 1f;
    [SerializeField] public float distance_To_Player;
    [SerializeField] public float range_To_Chase_Player = 7f;
    [SerializeField] protected bool is_Attacking;
    [SerializeField] protected bool is_Moving;
    [SerializeField] public bool is_Spawned;
    [Space(10)]
    [SerializeField] int min_Gold_OnDeath;
    [SerializeField] int max_Gold_OnDeath;

    [Space(10)]
    [Header("Timer")]
    [SerializeField] protected float attack_Timer;
    [SerializeField] protected float delay_Between_Attacks = 3f;
    [SerializeField] protected bool refresh_Attack_Timer;

    [Header("Inside Reference")]
    [SerializeField] protected Animator anim;
    [SerializeField] protected SpriteRenderer theBody;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] public NavMeshAgent agent;

    public Player_Detection player_Detection;
    public Enemy_Stats stats;

    // Boss bedzie posiadal bardziej skomplikowany skrypt, wiec wymagane jest rozdzielenie
    public abstract void SetMoveTarget(Player target);
    public Transform GetMoveTarget()
    {
        return move_target;
    }


    protected void CheckDistanceToPlayer()
    {
        distance_To_Player = Vector3.Distance(transform.position, move_target.transform.position);
    }
    protected abstract void AttackPlayer();
    public abstract void TakeDamage(float val);
    protected void RefreshAttackTimer()
    {
        attack_Timer += Time.deltaTime;
        if (attack_Timer > delay_Between_Attacks)
        {
            attack_Timer = 0;
            refresh_Attack_Timer = false;
        }
    }
    public void Death()
    {
        if (is_Spawned == false)
        {
            int rand = Random.Range(min_Gold_OnDeath, max_Gold_OnDeath);
            ScoreTable.ins.AddGold(rand);
        }
        else
        {
            GetComponentInParent<Enemy_Spawner>().RemoveMe(this);
        }

        AI_Manager.ins.RemoveFromChasing(this);
        Destroy(gameObject);

    }
}
