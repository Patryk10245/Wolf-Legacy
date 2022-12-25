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
    void Update()
    {
        if (refresh_Attack_Timer == true)
        {
            RefreshAttackTimer();
        }

        if (move_target == null)
        {
            return;
        }

        CheckDistanceToPlayer();

        if (distance_To_Player <= attack_Distance && refresh_Attack_Timer == false)
        {
            AttackPlayer();
        }
    }

    public override void TakeDamage(float val)
    {
        stats.TakeDamage(val);
    }

    protected override void AttackPlayer()
    {
        GameObject temp = Instantiate(projectile_Prefab);
        temp.transform.position = projectile_SpawnPoint.position;

        Projectile_Control proj_control = temp.GetComponent<Projectile_Control>();
        proj_control.dir = (move_target.transform.position - projectile_SpawnPoint.position).normalized;
        proj_control.dir.z = 0; // NavMesh generowany jest wyzej, co sprawia że różni się wysokosć miedzy graczami a przeciwnikami. Sprawia że pociski nie wpadaja pod mape po wleceniu w gracza
        proj_control.initial_pos = projectile_SpawnPoint.position;
        proj_control.speed = projectile_Speed;
        proj_control.max_Fly_Distance = 15f;

        refresh_Attack_Timer = true;
    }

    public override void SetMoveTarget(Player target)
    {
        move_target = target.transform;
    }

    private void Start()
    {
        if (anim == null) anim = GetComponent<Animator>();
        if (theBody == null) theBody = GetComponent<SpriteRenderer>();
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (agent == null) agent = GetComponent<NavMeshAgent>();
        if (player_Detection == null) player_Detection = GetComponentInChildren<Player_Detection>();
        if (stats == null) stats = GetComponent<Enemy_Stats>();

        agent.speed = move_Speed;
        agent.stoppingDistance = attack_Distance;
        rb.gravityScale = 0;
    }

}
