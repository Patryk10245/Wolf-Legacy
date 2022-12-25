using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ENUM_ActionState
{
    idle,
    chasing,
    waiting_for_attack,
    attacking
}

public class Enemy_Melee : Enemy_BaseClass
{
    [Space(15)]
    [SerializeField] ENUM_ActionState action_state;
    [SerializeField] Enemy_Hit_Collider hit_collider;
    [SerializeField] float obstacle_avoidance_radius = 0.3f;


    public override void SetMoveTarget(Player target)
    {
        move_target = target.transform;
    }

    public override void TakeDamage(float val)
    {
        stats.TakeDamage(val);
    }

    protected override void AttackPlayer()
    {
        //Debug.Log("Starting Attack Animation");
        anim.SetTrigger("isFlying");
        refresh_Attack_Timer = true;

        is_Attacking = true;

        action_state = ENUM_ActionState.attacking;
    }

    private void Update()
    {
        if (refresh_Attack_Timer == true)
        {
            action_state = ENUM_ActionState.waiting_for_attack;
            RefreshAttackTimer();
        }

        if(move_target == null)
        {
            return;
        }
    
        CheckDistanceToPlayer();

        if (distance_To_Player <= attack_Distance && refresh_Attack_Timer == false)
        {
            //Debug.Log("Attackign player, distance = " + distance_To_Player + "\n refresh_bool = " + refresh_Attack_Timer);
            AttackPlayer();
        }
        //SetAnimations();
    }

    void SetAnimations()
    {
        if(is_Attacking)
        {
            anim.SetTrigger("isFlying");
        }

    }
    public void EVENT_Animation_Attack()
    {
        //Debug.Log("Event Animation Attack, ME = " + name);
        if(hit_collider.is_TouchingPlayer == true)
        {
            hit_collider.touched_player.TakeDamage(stats.Damage);
        }
    }

    private void Start()
    {
        if(anim == null) anim = GetComponent<Animator>();
        if(theBody == null) theBody = GetComponent<SpriteRenderer>();
        if(rb == null) rb = GetComponent<Rigidbody2D>();
        if(agent == null) agent = GetComponent<NavMeshAgent>();
        if(player_Detection == null) player_Detection = GetComponentInChildren<Player_Detection>();
        if(stats == null) stats = GetComponent<Enemy_Stats>();
        if (hit_collider == null) hit_collider = GetComponentInChildren<Enemy_Hit_Collider>();

        agent.speed = move_Speed;
        agent.stoppingDistance = attack_Distance;
        rb.gravityScale = 0;
        agent.radius = obstacle_avoidance_radius;
    }
}
