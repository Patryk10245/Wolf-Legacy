using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Enemy_Stats))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Stop_Rotation))]
public abstract class Enemy_BaseClass : MonoBehaviour
{
    [SerializeField]protected Transform move_target;
    protected Vector3 moveDirection;

    [Header("Specifics")]
    [SerializeField] protected float move_Speed = 5f;
    [SerializeField] public float attack_Distance = 1f;
    public float distance_To_Player;
    [SerializeField] protected float delay_Between_Attacks = 3f;
    protected float attack_Timer;
    protected bool refresh_Attack_Timer;

    [Header("State")]
    public bool is_Spawned;
    public bool is_dying;
    protected bool is_Attacking;
    protected bool is_Moving;

    [Space(10)]
    [SerializeField] int minGoldOnDeath;
    [SerializeField] int maxGoldOnDeath;
     

    [Space(10)]
    

    protected Animator anim;
    protected SpriteRenderer theBody;
    protected Rigidbody2D rb;
    [HideInInspector] public NavMeshAgent agent;

    [Space(15)]
    [Tooltip("Child object with trigger collider")]
    public Enemy_Player_Detection player_Detection;
    [HideInInspector] public Enemy_Stats stats;

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
    public void PlayDeathAnimation()
    {
        is_dying = true;
        anim.SetTrigger("isDying");
    }
    public void AnimEvent_Death()
    {
        Death();
    }
    public void Death()
    {
        if (is_Spawned == false)
        {
            int rand = Random.Range(minGoldOnDeath, maxGoldOnDeath);
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
