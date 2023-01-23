using UnityEngine;
using UnityEngine.AI;

public enum ENUM_EnemyState
{
    idle,
    chasing,
    attacking,
    dying
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Stop_Rotation))]
[RequireComponent(typeof(Enemy_Stats))]
public abstract class Enemy_BaseClass : MonoBehaviour
{
    [Header("Scene Reference")]
    [SerializeField] protected Player move_target;
    [SerializeField] protected Vector3 moveDirection;

    [Header("Specifics")]
    public float move_Speed = 5f;
    public float attack_Distance = 1f;
    public float distance_To_Player;
    [Tooltip("Should be higher by at least 1 than detection radius")]
    public float chasePlayerDistance = 8;

    [SerializeField] protected float delay_Between_Attacks = 3f;
    protected float attack_Timer;
    protected bool refresh_Attack_Timer;

    [Header("States")]
    public ENUM_EnemyState currentEnemyState;
    protected bool is_Attacking;
    protected bool is_Moving;
    [HideInInspector] public bool is_dying;

    [Space(10)]
    public int min_Gold_OnDeath = 1;
    public int max_Gold_OnDeath = 1;

    [Header("Inside Reference")]
    [SerializeField] protected Animator anim;
    [SerializeField] protected SpriteRenderer theBody;
    [SerializeField] protected Rigidbody2D rb;
    public NavMeshAgent agent;
    public Enemy_Stats stats;

    [SerializeField] GameObject deathParticleEffect;

    public void SetMoveTarget(Player player)
    {
        move_target = player;
    }
    public abstract void TakeDamage(float val, ENUM_AttackType attackType, Player source);
    protected void RefreshAttack()
    {
        if (refresh_Attack_Timer == true)
        {
            attack_Timer += Time.deltaTime;
            if (attack_Timer >= delay_Between_Attacks)
            {
                attack_Timer = 0;
                refresh_Attack_Timer = false;
            }

        }
    }

    protected void CheckDistanceToPlayers()
    {
        distance_To_Player = Vector2.Distance(move_target.transform.position, transform.position);

        float smallest_distance = distance_To_Player;
        Player closest_player = move_target;
        foreach (Player player in Player_Manager.ins.playerList)
        {
            float distance = Vector2.Distance(player.transform.position, transform.position);
            if (distance < smallest_distance)
            {
                smallest_distance = distance;
                closest_player = player;
            }
        }
        if (distance_To_Player >= smallest_distance - 1)
        {
            move_target = closest_player;
            distance_To_Player = smallest_distance;
        }
        if (distance_To_Player > chasePlayerDistance)
        {
            move_target = null;
            agent.SetDestination(transform.position);
        }
    }

    protected void ApplyAnimation()
    {
        switch (currentEnemyState)
        {
            case ENUM_EnemyState.idle:
                anim.SetTrigger("isIdle");
                break;
            case ENUM_EnemyState.chasing:
                anim.SetTrigger("isChasing");
                break;
            case ENUM_EnemyState.attacking:
                anim.SetTrigger("isAttacking");
                is_Attacking = true;
                break;
            case ENUM_EnemyState.dying:
                anim.SetTrigger("isDying");
                break;
        }

    }

    public abstract void MeleeAttack_Action();
    public abstract void RangedAttack_Action();
    public void Death()
    {
        ScoreTable.ins.AddKill();
        AudioManager.ins.Play_EnemyHurt();
        int random_gold = Random.Range(min_Gold_OnDeath, max_Gold_OnDeath);
        ScoreTable.ins.AddGold(random_gold);

        Destroy(gameObject);

    }
    protected void ChangeState(ENUM_EnemyState new_state)
    {
        if (currentEnemyState != new_state)
        {
            currentEnemyState = new_state;
            ApplyAnimation();
        }
    }
    public void CreateParticles()
    {
        Instantiate(deathParticleEffect, transform.position, transform.rotation);
    }
}
