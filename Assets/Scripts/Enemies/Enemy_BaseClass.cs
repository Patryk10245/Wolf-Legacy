using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

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
    //[SerializeField] protected Player chase_target;

    [Header("Specifics")]
    [SerializeField] protected float move_Speed = 5f;
    [SerializeField] public float attack_Distance = 1f;
    [SerializeField] public float distance_To_Player;
    [Tooltip("Should be higher by at least 1 than detection radius")]
    [SerializeField] public float chasePlayerDistance = 8;

    [SerializeField] protected float delay_Between_Attacks = 3f;
    protected float attack_Timer;
    protected bool refresh_Attack_Timer;

    [Header("States")]
    public ENUM_EnemyState currentEnemyState;
    protected bool is_Attacking;
    protected bool is_Moving;
    [HideInInspector] public bool is_Spawned;
    [HideInInspector] public bool is_dying;

    [Space(10)]
    [SerializeField] int min_Gold_OnDeath = 1;
    [SerializeField] int max_Gold_OnDeath = 1;

    [Header("Inside Reference")]
    [SerializeField] protected Animator anim;
    [SerializeField] protected SpriteRenderer theBody;
    [SerializeField] protected Rigidbody2D rb;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Enemy_Stats stats;

    public void SetMoveTarget(Player player)
    {
        move_target = player;
    }
    public abstract void TakeDamage(float val, ENUM_AttackType attackType);
    protected void RefreshAttack()
    {
        if(refresh_Attack_Timer == true)
        {
            attack_Timer += Time.deltaTime;
            if(attack_Timer >= delay_Between_Attacks)
                {
                attack_Timer = 0;
                refresh_Attack_Timer = false;
            }

        }
    }

    protected void CheckDistanceToPlayers()
    {
        // Variables to store closest player
        distance_To_Player = Vector2.Distance(move_target.transform.position, transform.position);

        float smallest_distance = distance_To_Player;
        Player closest_player = move_target;
        // Deciding on who is closer
        foreach(Player player in Player_Manager.ins.playerList)
        {
            float distance = Vector2.Distance(player.transform.position, transform.position);
            if(distance < smallest_distance)
            {
                smallest_distance = distance;
                closest_player = player;
            }
        }

        // If closest player is closer, choose to chase him
        if(distance_To_Player >= smallest_distance -1)
        {
            //Debug.Log("Closest player");
            move_target = closest_player;
            distance_To_Player = smallest_distance;
            //Debug.Log("closest player  = " + closest_player);
            //Debug.Log("smallest distance = " + smallest_distance);
        }

        //Debug.Log("Distance to player = " + distance_To_Player);
        // Player too far. Abort chase
        if (distance_To_Player > chasePlayerDistance)
        {
            //Debug.Log("Distance too small");
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
                //anim.SetBool("isIdle", true);
                //anim.SetBool("isChasing",false);
                //anim.SetBool("isAttacking",false);
                //anim.SetBool("isDying",false);
                break;
            case ENUM_EnemyState.chasing:
                anim.SetTrigger("isChasing");
                //anim.SetBool("isIdle", false);
                //anim.SetBool("isChasing", true);
                //anim.SetBool("isAttacking", false);
                //anim.SetBool("isDying", false);
                break;
            case ENUM_EnemyState.attacking:
                anim.SetTrigger("isAttacking");
                is_Attacking = true;
                //anim.SetBool("isIdle", false);
                //anim.SetBool("isChasing", false);
                //anim.SetBool("isAttacking", true);
                //anim.SetBool("isDying", false);
                break;
            case ENUM_EnemyState.dying:
                anim.SetTrigger("isDying");
                //anim.SetBool("isIdle", false);
                //anim.SetBool("isChasing", false);
                //anim.SetBool("isAttacking", false);
                //anim.SetBool("isDying", true);
                break;
        }

    }

    public abstract void MeleeAttack_Action();
    public abstract void RangedAttack_Action();
    public void Death()
    {
        //Debug.LogWarning("Smierc przeciwnika nie skonczona");
        ScoreTable.ins.AddKill();

        if(is_Spawned == false)
        {
            int random_gold = Random.Range(min_Gold_OnDeath, max_Gold_OnDeath);
            ScoreTable.ins.AddGold(random_gold);
            
        }
        else
        {
            GetComponentInParent<Enemy_Spawner>().RemoveMe(this);
        }

        Destroy(gameObject);



    }
    protected void ChangeState(ENUM_EnemyState new_state)
    {
        if (currentEnemyState != new_state)
        {
            //Debug.Log("Changing State to " + new_state.ToString()) ;
            currentEnemyState = new_state;
            ApplyAnimation();
        }
    }
}
