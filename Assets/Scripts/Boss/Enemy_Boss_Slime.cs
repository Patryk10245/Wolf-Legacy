using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public enum ENUM_SlimeBossState
{
    idle,
    moving,
    spawning,
    shooting,
    defending,
    dying
}


public class Enemy_Boss_Slime : Enemy_BaseClass
{
    public Image healthBar;
    [Space(10)]
    public ENUM_DuckBossState bossState;
    public ENUM_current_state currentActionState = ENUM_current_state.ready_to_exit;
    int last_action;
    [Space(10)]
    [SerializeField] float changeTargetTime = 15;
    float change_target_timer;



    [Header("Idle Action")]
    [SerializeField] float minIdleTime = 1;
    [SerializeField] float maxIdleTime = 3;
    float idle_timer;
    float idle_time;
    [SerializeField] Vector4 arenaBounds;


    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        if (move_target == null)
        {
            return;
        }

        ChangeTarget();

        distance_To_Player = Vector2.Distance(move_target.transform.position, transform.position);


        RotateTowardsWalkDirection();
    }




    public override void MeleeAttack_Action()
    {

    }

    public override void RangedAttack_Action()
    {
    }

    public override void TakeDamage(float val, ENUM_AttackType attackType)
    {
        if (bossState == ENUM_DuckBossState.attack_jumping && attackType == ENUM_AttackType.melee)
        {
            return;
        }
        stats.TakeDamage(val);
        if (stats.currentHealth <= 0)
        {
            is_dying = true;
            currentEnemyState = ENUM_EnemyState.dying;
            ApplyAnimation();
        }
        healthBar.fillAmount = stats.currentHealth / stats.maxHealth;
    }

    void ChangeTarget()
    {
        if (Player_Manager.ins.playerList.Count <= 1)
        {
            return;
        }

        change_target_timer += Time.deltaTime;
        if (change_target_timer >= changeTargetTime)
        {
            change_target_timer = 0;
            int rand = Random.Range(0, Player_Manager.ins.playerList.Count);

            move_target = Player_Manager.ins.playerList[rand];
        }
    }

    void RotateTowardsWalkDirection()
    {
        Vector3 direction = agent.destination - transform.position;
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (direction.x < 0)
        {
            transform.localScale = Vector3.one;
        }
    }
}

