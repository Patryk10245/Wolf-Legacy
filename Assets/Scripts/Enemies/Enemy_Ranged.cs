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

}
