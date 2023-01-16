using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public enum ENUM_MageBossState
{
    idle,
    projectileWaves,
    thunders,
    areaAttack,
    spawning,
    dying
}
public class Enemy_Boss_Mage : Enemy_BaseClass
{
    [Space(20)]
    [SerializeField] Boss_Area bossArea;

    public Image healthBar;
    public Animator healthBarAnimator;
    public ENUM_MageBossState bossState;
    int last_action;
    public ENUM_current_state currentActionState = ENUM_current_state.ready_to_exit;
    [SerializeField] float changeTargetTime = 15;
    float change_target_timer;

    [Header("Idle Action")]
    [SerializeField] float minIdleTime = 1;
    [SerializeField] float maxIdleTime = 3;
    float idle_timer;
    float idle_time;

    [Header("Projectile Waves")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject waveSpotParent;
    [SerializeField] Transform[] waveProjectileSpots;
    [SerializeField] float waveProjectileSpeed;
    [SerializeField] int waveDamage;
    [SerializeField] int waveCount;
    [SerializeField] float waveTimeDelay;
    [SerializeField]float waveTimer;
    int wavesShot;

    [Header("Thunder AOE")]
    [SerializeField] GameObject thunderPrefab;
    [SerializeField] int thunderDamage;
    [SerializeField] int thunderCount;
    [SerializeField] float thunderDelay = 1.5f;
    [SerializeField] float thunderTimer;
    int thunderShot;

    [Header("Area AOE")]
    [SerializeField] Animator areaPattern;
    [SerializeField] float areaPatternTime;
    [SerializeField] float areaTimer;

    [Header("Spawning Enemies")]
    [SerializeField] GameObject enemyToSpawn;
    [SerializeField] GameObject placeToSpawnEnemy;
    [SerializeField] int spawningAmount;
    int spawningDone;
    [SerializeField] float spawningDelay;
     float spawningTimer;

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
        if (Game_State.gamePaused)
            return;

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
                        bossState = ENUM_MageBossState.idle;
                        break;
                    case 1:
                        bossState = ENUM_MageBossState.projectileWaves;
                        break;
                    case 2:
                        bossState = ENUM_MageBossState.thunders;
                        break;
                    case 3:
                        bossState = ENUM_MageBossState.areaAttack;
                        break;
                    case 4:
                        bossState = ENUM_MageBossState.spawning;
                        break;
                    default:
                        break;
                }
                currentActionState = ENUM_current_state.preparation;
            }
            while (rand == last_action);
        }

        switch (bossState)
        {
            case ENUM_MageBossState.idle:
                Action_Idle();
                break;
            case ENUM_MageBossState.projectileWaves:
                Action_ProjectileWaves();
                break;
            case ENUM_MageBossState.thunders:
                Action_Thunder();
                break;
            case ENUM_MageBossState.dying:
                Action_Dying();
                break;
            case ENUM_MageBossState.areaAttack:
                Action_AreaAttack();
                break;
            case ENUM_MageBossState.spawning:
                Action_Spawning();
                break;
            default:
                Debug.LogError("UNSPECIFIED BOSS STATE");
                break;
        }


        RotateTowardsWalkDirection();

    }

    private void Action_Dying()
    {
        switch (currentActionState)
        {
            case ENUM_current_state.preparation:
                currentActionState = ENUM_current_state.finishing;
                break;
            case ENUM_current_state.working:
                break;
            case ENUM_current_state.finishing:
                bossArea.DeactivateBlockades();
                break;
            case ENUM_current_state.ready_to_exit:
                break;
        }
    }

    private void Action_Idle()
    {
        last_action = 0;
        switch (currentActionState)
        {
            case ENUM_current_state.preparation:
                currentActionState = ENUM_current_state.ready_to_exit;
                break;
            case ENUM_current_state.working:
                break;
            case ENUM_current_state.finishing:
                break;
            case ENUM_current_state.ready_to_exit:
                break;
        }
    }

    private void Action_ProjectileWaves()
    {
        //Debug.Log("action projectile waves");
        switch (currentActionState)
        {
            case ENUM_current_state.preparation:
                //Debug.Log("Preparation");
                if(wavesShot < waveCount)
                {
                    foreach (Transform spot in waveProjectileSpots)
                    {
                        GameObject temp = Instantiate(projectilePrefab);
                        temp.transform.position = spot.position;
                        Vector3 dir = (temp.transform.position - gameObject.transform.position).normalized;
                        temp.GetComponent<Enemy_Projectile>().rb.AddForce(dir * waveProjectileSpeed);
                        currentActionState = ENUM_current_state.working;
                    }
                    GameObject lone = Instantiate(projectilePrefab);
                    lone.transform.position = transform.position;
                    Vector3 lonedir = (move_target.transform.position - gameObject.transform.position).normalized;
                    lone.GetComponent<Enemy_Projectile>().rb.AddForce(lonedir * waveProjectileSpeed);
                    wavesShot++;
                    //waveSpotParent.transform.Rotate(new Vector3(0,0,5));
                }
                else
                {
                    currentActionState = ENUM_current_state.finishing;
                }
                break;
            case ENUM_current_state.working:
                //Debug.Log("Working");
                waveTimer += Time.deltaTime;
                //Debug.Log("wave timer = " + waveTimer);
                //Debug.Log("delay = " + waveTimeDelay);
                if(waveTimer >= waveTimeDelay)
                {
                    currentActionState = ENUM_current_state.preparation;
                    waveTimer = 0;
                }
                break;
            case ENUM_current_state.finishing:
                //Debug.Log("Finishing");
                waveTimer = 0;
                wavesShot = 0;
                last_action = 1;
                currentActionState = ENUM_current_state.ready_to_exit;
                break;
            case ENUM_current_state.ready_to_exit:
                break;
        }
    }

    private void Action_Thunder()
    {
        switch (currentActionState)
        {
            case ENUM_current_state.preparation:
                if(thunderShot < thunderCount)
                {
                    GameObject temp = Instantiate(thunderPrefab);
                    temp.transform.position = move_target.transform.position;
                    Boss_Mage_ThunderObject thunder = temp.GetComponent<Boss_Mage_ThunderObject>();
                    thunder.damage = thunderDamage;

                    thunderShot++;
                    currentActionState = ENUM_current_state.working;
                }
                else
                {
                    currentActionState = ENUM_current_state.finishing;
                }    
                break;

            case ENUM_current_state.working:
                thunderTimer += Time.deltaTime;
                if (thunderTimer >= thunderDelay)
                {
                    thunderTimer = 0;
                    currentActionState = ENUM_current_state.preparation;
                }
                break;

            case ENUM_current_state.finishing:
                thunderTimer = 0;
                thunderShot = 0;
                last_action = 2;
                currentActionState = ENUM_current_state.ready_to_exit;
                break;

            case ENUM_current_state.ready_to_exit:
                break;
        }
    }

    private void Action_AreaAttack()
    {
        switch (currentActionState)
        {
            case ENUM_current_state.preparation:
                areaPattern.SetTrigger("areaAttack");
                currentActionState = ENUM_current_state.working;
                break;

            case ENUM_current_state.working:
                areaTimer += Time.deltaTime;
                if (areaTimer >= areaPatternTime)
                {
                    currentActionState = ENUM_current_state.finishing;
                }
                break;

            case ENUM_current_state.finishing:
                last_action = 3;
                areaTimer = 0;
                currentActionState = ENUM_current_state.ready_to_exit;
                break;

            case ENUM_current_state.ready_to_exit:
                break;
        }
    }
    
    private void Action_Spawning()
    {
        switch(currentActionState)
        {
            case ENUM_current_state.preparation:
                currentActionState = ENUM_current_state.working;
                break;
            case ENUM_current_state.working:
                spawningTimer += Time.deltaTime;
                if(spawningTimer >= spawningDelay)
                {
                    spawningTimer -= spawningDelay;

                    GameObject temp = Instantiate(enemyToSpawn, placeToSpawnEnemy.transform.position, transform.rotation);
                    temp.transform.SetParent(placeToSpawnEnemy.transform);

                    Enemy_BaseClass enemy = temp.GetComponent<Enemy_BaseClass>();
                    enemy.is_Spawned = true;
                    placeToSpawnEnemy.GetComponent<Enemy_Spawner>().listOfCreatedEnemies.Add(enemy);

                    temp.GetComponent<NavMeshAgent>().SetDestination(move_target.transform.position);
                    
                    spawningDone++;

                    if(spawningDone >= spawningAmount)
                    {
                        currentActionState = ENUM_current_state.finishing;
                    }
                }
                break;
            case ENUM_current_state.finishing:
                spawningDone = 0;
                spawningTimer = 0;
                currentActionState = ENUM_current_state.ready_to_exit;
                break;
            case ENUM_current_state.ready_to_exit:
                break;
        }
    }

    public override void TakeDamage(float val, ENUM_AttackType attackType)
    {
        stats.TakeDamage(val);
        if (stats.currentHealth <= 0)
        {
            bossState = ENUM_MageBossState.dying;
            currentActionState = ENUM_current_state.preparation;
        }
        healthBar.fillAmount = stats.currentHealth / stats.maxHealth;
        healthBarAnimator.SetTrigger("tilt");
    }

    void RotateTowardsWalkDirection()
    {
        Vector3 direction = agent.destination - transform.position;
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(-4, 4, 1);
        }
        else if (direction.x < 0)
        {
            transform.localScale = Vector3.one * 4;
        }
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

    // ======================================================================
    public override void MeleeAttack_Action()
    {
        throw new System.NotImplementedException();
    }

    public override void RangedAttack_Action()
    {
        throw new System.NotImplementedException();
    }
}
