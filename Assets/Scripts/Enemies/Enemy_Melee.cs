using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Melee : Enemy_BaseClass
{
    [SerializeField] float obstacle_avoidance_radius = 0.3f;

    private void Update()
    {
        if(currentEnemyState == ENUM_EnemyState.dying)
        {
            return;
        }

        RefreshAttack();

        if(move_target == null)
        {
            ChangeState(ENUM_EnemyState.idle);
            return;
        }

        CheckDistanceToPlayers();

        if (move_target == null)
        {
            ChangeState(ENUM_EnemyState.idle);
            return;
        }

        agent.SetDestination(move_target.transform.position);
        // Helps with OverStepping
        if(distance_To_Player < attack_Distance)
        {
            agent.velocity = Vector3.zero;
        }
        
        if(distance_To_Player <= attack_Distance)
        {
            Attack();
        }
        else
        {
            ChangeState(ENUM_EnemyState.chasing);
        }
    }

    void Attack()
    {
        if (refresh_Attack_Timer == false && is_Attacking == false)
        {
            ChangeState(ENUM_EnemyState.attacking);
        }
        else
        {
            ChangeState(ENUM_EnemyState.chasing);

        }
    }
   public override void MeleeAttack_Action()
    {
        refresh_Attack_Timer = true;
        is_Attacking = false;

        if (distance_To_Player <= attack_Distance)
        {
            move_target.TakeDamage(stats.damage);
            
        }
    }

    public override void TakeDamage(float val)
    {
        stats.TakeDamage(val);
        if (stats.currentHealth <= 0)
        {
            is_dying = true;
            currentEnemyState = ENUM_EnemyState.dying;
            ApplyAnimation();
        }
    }
    




    private void Start()
    {
        if(anim == null) anim = GetComponent<Animator>();
        if(theBody == null) theBody = GetComponent<SpriteRenderer>();
        if(rb == null) rb = GetComponent<Rigidbody2D>();
        if(agent == null) agent = GetComponent<NavMeshAgent>();
        if(stats == null) stats = GetComponent<Enemy_Stats>();

        agent.speed = move_Speed;
        agent.stoppingDistance = attack_Distance;
        rb.gravityScale = 0;
        agent.radius = obstacle_avoidance_radius;
    }

    public override void RangedAttack_Action()
    {
        // UNUSED
    }
}
