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
    jumping,
    dying
}


public class Enemy_Boss_Slime : Enemy_BaseClass
{


    public Image healthBar;
    [Space(10)]
    public ENUM_SlimeBossState bossState;
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

    [Header("Move Action")]
    [SerializeField] Transform[] arenaCorners;
    [SerializeField] Vector4 arenaBounds;

    [Header("Shooting Action")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform middleOfArena;
    [SerializeField] float timeBetweenShots = 0.1f;
    float shootingTimer;
    [SerializeField] float projectileSpeed = 150;
    [SerializeField] float projectileDamage = 1;
    [SerializeField] int stopAfterThisManyShots = 150;
    [SerializeField] float aimingCorrection = 1.5f;
    float projectileDeathTime = 3;
    int shotCount;
    Player shootTarget;

    [Header("Spawning Action")]
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int amountToSpawn;
    int amountSpawned;
    [SerializeField] Transform placeToSpawn;
    [SerializeField] float spawnDelay;
    float spawnTimer;

    [Header("Jumping Action")]
    [SerializeField] Transform[] jumpPositions;




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


        if (currentActionState == ENUM_current_state.ready_to_exit)
        {
            int rand;
            do
            {
                rand = Random.Range(0, 5);

                switch (rand)
                {
                    case 0:
                        bossState = ENUM_SlimeBossState.idle;   
                        break;
                    case 1:
                        bossState = ENUM_SlimeBossState.moving;
                        break;
                    case 2:
                        bossState = ENUM_SlimeBossState.shooting;
                        break;
                    case 3:
                        bossState = ENUM_SlimeBossState.spawning;
                        break;
                    case 4:
                        bossState = ENUM_SlimeBossState.jumping;
                        break;
                    default:
                        break;
                }
                currentActionState = ENUM_current_state.preparation;
            }
            while (rand == last_action);
        }

        switch(bossState)
        {
            case ENUM_SlimeBossState.idle:
                Action_Idle();
                break;
            case ENUM_SlimeBossState.moving:
                Action_Moving();
                break;
            case ENUM_SlimeBossState.shooting:
                Action_Shooting();
                break;
            case ENUM_SlimeBossState.spawning:
                Action_Spawning();
                break;
            case ENUM_SlimeBossState.jumping:
                Action_Jumping();
                break;
            case ENUM_SlimeBossState.dying:
                Action_Dying();
                break;
            default:
                Debug.LogError("UNSPECIFIED BOSS STATE");
                break;
        }


        RotateTowardsWalkDirection();
    }


    void Action_Idle()
    {
        switch (currentActionState)
        {
            case ENUM_current_state.preparation:
                //Debug.Log("Idle State");
                idle_time = Random.Range(minIdleTime, maxIdleTime);
                idle_timer = 0;
                anim.SetTrigger("isIdle");
                currentActionState = ENUM_current_state.working;
                break;

            case ENUM_current_state.working:
                idle_timer += Time.deltaTime;

                if (idle_timer >= idle_time)
                {
                    currentActionState = ENUM_current_state.finishing;
                }
                break;

            case ENUM_current_state.finishing:
                currentActionState = ENUM_current_state.ready_to_exit;
                last_action = 0;
                break;

            case ENUM_current_state.ready_to_exit:
                break;
            default:
                //Debug.LogError("ERROR. UNKOWN ACTION STATE");
                break;
        }
    }
    void Action_Moving()
    {
        switch (currentActionState)
        {
            case ENUM_current_state.preparation:
                //Debug.Log("BEGIN Moving State");
                Vector3 random_spot = new Vector3(Random.Range(arenaCorners[0].position.x, arenaCorners[1].position.x), Random.Range(arenaCorners[0].position.y, arenaCorners[1].position.y), transform.position.z);
                //Debug.Log("random spot = " + random_spot);
                //Debug.Log("position = " + gameObject.transform.position);
                agent.SetDestination(random_spot);
                anim.SetTrigger("isMoving");
                currentActionState = ENUM_current_state.working;
                break;
            case ENUM_current_state.working:
                //Debug.Log("agent dest = " + agent.destination);
                //Debug.Log("position = " + gameObject.transform.position);
                if (Vector3.Distance(gameObject.transform.position, agent.destination) < 1.5f)
                {
                    currentActionState = ENUM_current_state.finishing;
                }
                break;
            case ENUM_current_state.finishing:
                //Debug.Log("walking finishing");
                anim.SetTrigger("exitAnimation");
                agent.SetDestination(transform.position);
                currentActionState = ENUM_current_state.ready_to_exit;
                //Debug.Log("END Moving State");
                last_action = 1;
                break;
            case ENUM_current_state.ready_to_exit:
                break;

        }
    }
    void Action_Shooting()
    {
        switch (currentActionState)
        {
            case ENUM_current_state.preparation:
                agent.SetDestination(middleOfArena.position);

                if(agent.remainingDistance < 1)
                {
                    shotCount = 0;
                    shootTarget = move_target.GetComponent<Player>();
                    currentActionState = ENUM_current_state.working;
                }
                break;
            case ENUM_current_state.working:
                shootingTimer += Time.deltaTime;
                if(shootingTimer >= timeBetweenShots)
                {
                    shootingTimer -= timeBetweenShots;
                    GameObject temp = Instantiate(projectilePrefab);
                    temp.transform.position = gameObject.transform.position;
                    Enemy_Projectile proj = temp.GetComponent<Enemy_Projectile>();
                    Vector3 direction = move_target.transform.position + (new Vector3(shootTarget.controller.moveInput.x, shootTarget.controller.moveInput.y, 0) *aimingCorrection );
                    proj.flyDirection = direction - gameObject.transform.position;
                    proj.speed = projectileSpeed;
                    proj.damage = projectileDamage;
                    proj.stopTimerAt = projectileDeathTime;
                    shotCount++;
                }

                if(shotCount >= stopAfterThisManyShots)
                {
                    currentActionState = ENUM_current_state.finishing;
                }
                break;
            case ENUM_current_state.finishing:
                last_action = 2;
                currentActionState = ENUM_current_state.ready_to_exit;
                break;
            case ENUM_current_state.ready_to_exit:
                break;
        }
    }
    void Action_Spawning()
    {
        switch (currentActionState)
        {
            case ENUM_current_state.preparation:
                amountSpawned = 0;
                spawnTimer = 0;
                currentActionState = ENUM_current_state.working;
                break;
            case ENUM_current_state.working:
                spawnTimer += Time.deltaTime;
                if(spawnTimer >= spawnDelay)
                {
                    spawnTimer -= spawnDelay;
                    GameObject temp = Instantiate(enemyPrefab,placeToSpawn.transform.position,transform.rotation);
                    //temp.transform.position = placeToSpawn.transform.position;
                    temp.GetComponent<NavMeshAgent>().SetDestination(move_target.transform.position);
                    amountSpawned++;
                }

                if(amountSpawned >= amountToSpawn)
                {
                    currentActionState = ENUM_current_state.finishing;
                }
                break;
            case ENUM_current_state.finishing:
                currentActionState = ENUM_current_state.ready_to_exit;
                last_action = 3;
                break;
            case ENUM_current_state.ready_to_exit:
                break;
        }
    }
    void Action_Jumping()
    {
        switch (currentActionState)
        {
            case ENUM_current_state.preparation:
                currentActionState = ENUM_current_state.ready_to_exit;
                break;
            case ENUM_current_state.working:
                currentActionState = ENUM_current_state.ready_to_exit;
                break;
            case ENUM_current_state.finishing:
                currentActionState = ENUM_current_state.ready_to_exit;
                last_action = 4;
                break;
            case ENUM_current_state.ready_to_exit:
                break;
        }
    }

    void Action_Dying()
    {
        switch (currentActionState)
        {
            case ENUM_current_state.preparation:
                break;
            case ENUM_current_state.working:
                break;
            case ENUM_current_state.finishing:
                break;
            case ENUM_current_state.ready_to_exit:
                break;
        }
    }


    public void EVENT_Preparation()
    {
        //Debug.Log("Event Preparation");
        currentActionState = ENUM_current_state.preparation;
    }
    public void EVENT_Working()
    {
        //Debug.Log("Event Working");
        currentActionState = ENUM_current_state.working;
    }
    public void EVENT_Finishing()
    {
        //Debug.Log("Event Finishing");
        currentActionState = ENUM_current_state.finishing;
    }
    public void EVENT_Exiting()
    {
        //Debug.Log("Event Exiting");
        currentActionState = ENUM_current_state.ready_to_exit;
    }


    public override void MeleeAttack_Action()
    {

    }

    public override void RangedAttack_Action()
    {
    }

    public override void TakeDamage(float val, ENUM_AttackType attackType)
    {
        stats.TakeDamage(val);
        if (stats.currentHealth <= 0)
        {
            is_dying = true;
            Debug.LogWarning("NOT DYING CURRENTLY");
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

