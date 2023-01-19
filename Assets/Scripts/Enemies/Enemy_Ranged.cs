using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy_Ranged : Enemy_BaseClass
{
    [Header("Projectile Data")]
    [SerializeField] GameObject projectile_Prefab;
    [SerializeField] Transform projectile_SpawnPoint;
    [SerializeField] float projectile_Speed;

    public override void MeleeAttack_Action()
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
        if (anim == null) anim = GetComponent<Animator>();
        if (theBody == null) theBody = GetComponent<SpriteRenderer>();
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (agent == null) agent = GetComponent<NavMeshAgent>();
        if (stats == null) stats = GetComponent<Enemy_Stats>();

        agent.speed = move_Speed;
        agent.stoppingDistance = attack_Distance;
        rb.gravityScale = 0;
    }

    private void Update()
    {
        if (Game_State.gamePaused)
            return;

        if (currentEnemyState == ENUM_EnemyState.dying)
        {
            return;
        }

        RefreshAttack();

        if (move_target == null)
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
        RotateTowardsWalkDirection();
        // Helps with OverStepping
        if (distance_To_Player <= attack_Distance)
        {
            //Debug.Log("Distance to player = " + distance_To_Player);
            Attack();
            agent.velocity = Vector3.zero;
        }
        else
        {
            //Debug.Log("Distance too big, ELSE");
            ChangeState(ENUM_EnemyState.chasing);
            is_Attacking = false;
        }
        
    }


    void Attack()
    {
        if (refresh_Attack_Timer == true)
        {
            ChangeState(ENUM_EnemyState.idle);
        }

        if (refresh_Attack_Timer == false && is_Attacking == false)
        {
            ChangeState(ENUM_EnemyState.attacking);
            is_Attacking = true;
        }
    }

    // Called from animnation event
    public override void RangedAttack_Action()
    {
        Debug.Log("Ranged Attack Action");
        GameObject temp = Instantiate(projectile_Prefab);
        Vector3 dir = (move_target.transform.position - projectile_SpawnPoint.position).normalized;
        temp.transform.position = projectile_SpawnPoint.position;

        Enemy_Projectile projectile = temp.GetComponent<Enemy_Projectile>();
        projectile.flyDirection = dir;
        projectile.speed = projectile_Speed;
        projectile.rb.AddForce(dir * projectile_Speed);
        projectile.damage = stats.damage;
    }
    public void AnimEvent_EndAttack()
    {
        Debug.Log("anim event end attack");
        is_Attacking = false;
        refresh_Attack_Timer = true;
    }
    public override void TakeDamage(float val, ENUM_AttackType attackType, Player source)
    {
        stats.TakeDamage(val);

        if (move_target == null)
        {
            move_target = source;
            chasePlayerDistance *= 2;
        }

        if (stats.currentHealth <= 0)
        {
            agent.SetDestination(transform.position);
            is_dying = true;
            currentEnemyState = ENUM_EnemyState.dying;
            tag = "Untagged";
            ApplyAnimation();
        }
    }

    void RotateTowardsWalkDirection()
    {
        Vector3 direction = agent.destination - transform.position;
        if (direction.x > 0)
        {
            transform.localScale = Vector3.one * 1;
        }
        else if (direction.x < 0)
        {

            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

}
