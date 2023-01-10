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
        // Helps with OverStepping
        if (distance_To_Player < attack_Distance)
        {
            agent.velocity = Vector3.zero;
        }

        if (distance_To_Player <= attack_Distance)
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

    // Called from animnation event
    public override void RangedAttack_Action()
    {
        GameObject temp = Instantiate(projectile_Prefab);
        Vector3 dir = (move_target.transform.position - projectile_SpawnPoint.position).normalized;
        temp.transform.position = projectile_SpawnPoint.position;

        Enemy_Projectile projectile = temp.GetComponent<Enemy_Projectile>();
        projectile.flyDirection = dir;
        projectile.speed = projectile_Speed;
        projectile.damage = stats.damage;
    }
    public override void TakeDamage(float val, ENUM_AttackType attackType)
    {
        stats.TakeDamage(val);
        if (stats.currentHealth <= 0)
        {
            is_dying = true;
            currentEnemyState = ENUM_EnemyState.dying;
            ApplyAnimation();
        }
    }

}
