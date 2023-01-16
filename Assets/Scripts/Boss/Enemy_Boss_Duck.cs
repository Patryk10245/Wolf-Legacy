using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public enum ENUM_DuckBossState
{
    unseen,
    idle,
    moving,
    attack_rushing,
    attack_jumping,
    dash,
    dying
}
[System.Serializable]
public enum ENUM_current_state
{
    preparation,
    working,
    finishing,
    ready_to_exit
}


// To initate Fight, Set Move Target
public class Enemy_Boss_Duck : Enemy_BaseClass
{
    [Space(20)]
    [SerializeField] Boss_Area bossArea;

    public Boss_Duck_SpecialEffects specialEffects;
    public Image healthBar;
    public ENUM_DuckBossState bossState;
    int last_action;    

    public ENUM_current_state currentActionState = ENUM_current_state.ready_to_exit;
    bool action_firstLoop = true;
    bool can_move;

    [SerializeField] float changeTargetTime = 15;
    float change_target_timer;
    Vector3 direction;

    [Header("Dash Action")]
    [SerializeField] float dashForce;
    [SerializeField] float dashRechargeTime;
    float dash_timer;
    [SerializeField] bool dash_WaitingForRecharge;

    [Header("Idle Action")]
    [SerializeField] float minIdleTime = 1;
    [SerializeField] float maxIdleTime = 3;
    float idle_timer;
    float idle_time;
    [Header("Move Action")]
    [SerializeField] Transform[] arenaCorners;

    [Header("Jump Action")]
    [SerializeField] Vector3 playerPos;
    [SerializeField] float fallAreaRadius;
    [SerializeField] float knockBackForce;
    [SerializeField] float jumpDamage;

    [Header("Rush Aciton")]
    [SerializeField] float speedModifier;
    [SerializeField] float rushDamage;
    bool is_rushing;
    

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

    // ====================================================================== //

    public override void TakeDamage(float val, ENUM_AttackType attackType)
    {
        if(bossState == ENUM_DuckBossState.attack_jumping && attackType == ENUM_AttackType.melee)
        {
            return;
        }
        stats.TakeDamage(val);
        if (stats.currentHealth <= 0)
        {
            bossState = ENUM_DuckBossState.dying;
            currentActionState = ENUM_current_state.preparation;
        }
        healthBar.fillAmount = stats.currentHealth / stats.maxHealth;
    }


    void Update()
    {
        if (Game_State.gamePaused)
            return;

        if (move_target == null)
        {
            return;
        }

        ChangeTarget();

        

        DashRefreshing();
        //CheckDistanceToPlayers();
        distance_To_Player = Vector2.Distance(move_target.transform.position, transform.position);

        // Check if duck should dash away from player
        if (distance_To_Player <= 2)
        {
            if(dash_WaitingForRecharge == false)
            {
                if(currentActionState == ENUM_current_state.ready_to_exit)
                {
                    bossState = ENUM_DuckBossState.dash;
                    currentActionState = ENUM_current_state.preparation;
                    return;
                }
            }
        }

        // Decide randomly, which action should duck choose
        if(currentActionState == ENUM_current_state.ready_to_exit)
        {
            


            action_firstLoop = true;
            int rand;
            do
            {
                rand = Random.Range(0, 4);
                //Debug.Log("last = " + last_action + "| current = " + rand);

                if (last_action == 4) // If dashed, then move
                {
                    rand = 3;
                }

                switch (rand)
                {
                    case 0:
                        bossState = ENUM_DuckBossState.idle;
                        currentActionState = ENUM_current_state.preparation;
                        break;
                    case 1:
                        bossState = ENUM_DuckBossState.attack_rushing;
                        currentActionState = ENUM_current_state.preparation;
                        break;
                    case 2:
                        bossState = ENUM_DuckBossState.attack_jumping;
                        currentActionState = ENUM_current_state.preparation;
                        break;
                    case 3:
                        bossState = ENUM_DuckBossState.moving;
                        currentActionState = ENUM_current_state.preparation;
                        break;
                    default:
                        break;
                }
            }
            while (rand == last_action);

            
        }

        // Do current state              *Quack *Quack
        switch (bossState)
        {
            case ENUM_DuckBossState.unseen:
                Action_Unseen();
                break;
            case ENUM_DuckBossState.idle:
                Action_Idle();
                break;
            case ENUM_DuckBossState.moving:
                Action_Moving();
                break;
            case ENUM_DuckBossState.dash:
                Action_Dash();
                break;
            case ENUM_DuckBossState.attack_jumping:
                Action_Jumping();
                break;
            case ENUM_DuckBossState.attack_rushing:
                Action_Rushing();
                break;
            case ENUM_DuckBossState.dying:
                Action_Dying();
                break;
            default:
                Debug.LogError("ERROR. UKNOWN BOSS STATE");
                break;
        }



        RotateTowardsWalkDirection();

    }
    void ChangeTarget()
    {
        if(Player_Manager.ins.playerList.Count <= 1)
        {
            return;
        }

        change_target_timer += Time.deltaTime;
        if(change_target_timer >= changeTargetTime)
        {
            change_target_timer = 0;
            int rand = Random.Range(0, Player_Manager.ins.playerList.Count);

            move_target = Player_Manager.ins.playerList[rand];
        }
    }

    void RotateTowardsWalkDirection()
    {
        direction = agent.destination - transform.position;
        if(direction.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(direction.x < 0)
        {
            transform.localScale = Vector3.one;
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

    void DashRefreshing()
    {
        if(dash_WaitingForRecharge == true)
        {
            dash_timer += Time.deltaTime;
            if(dash_timer >= dashRechargeTime)
            {
                dash_timer = 0;
                dash_WaitingForRecharge = false;
            }

        }
    }

    void Action_Unseen()
    {

    }
    void Action_Idle()
    {
        switch (currentActionState)
        {
            case ENUM_current_state.preparation:
                //Debug.Log("Idle State");
                idle_time = Random.Range(minIdleTime, maxIdleTime);
                idle_timer = 0;
                can_move = false;
                anim.SetTrigger("isIdle");
                currentActionState = ENUM_current_state.working;
                break;

            case ENUM_current_state.working:
                idle_timer += Time.deltaTime;

                if(idle_timer >= idle_time)
                {
                    currentActionState = ENUM_current_state.finishing;
                }
                break;

            case ENUM_current_state.finishing:
                can_move = true;
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
                last_action = 3;
                break;
            case ENUM_current_state.ready_to_exit:
                break;
                
        }
        
    }
    void Action_Dash()
    {
        switch (currentActionState)
        {
            case ENUM_current_state.preparation:
                //Debug.Log("Dash State");
                Vector2 direction_to_player = (move_target.transform.position - transform.position).normalized;
                rb.AddForce(-direction_to_player * dashForce);
                can_move = false;
                currentActionState = ENUM_current_state.finishing;
                break;
            case ENUM_current_state.working:
                break;
            case ENUM_current_state.finishing:
                can_move = true;
                dash_WaitingForRecharge = true;
                agent.SetDestination(transform.position);
                //agent.velocity = Vector3.zero;
                //Debug.Log("END Dash State");
                last_action = 4;
                currentActionState = ENUM_current_state.ready_to_exit;
                break;
            case ENUM_current_state.ready_to_exit:
                break;
            default:
                Debug.LogError("ERROR. UNKOWN ACTION STATE");
                break;
        }
    }
    void Action_Jumping()
    {
        switch (currentActionState)
        {
            case ENUM_current_state.preparation:

                if(action_firstLoop == true)
                {
                    //Debug.Log("Jump State");
                    playerPos = move_target.gameObject.transform.position + new Vector3(0.5f, 0.5f, 0);
                    anim.SetTrigger("isJumping");
                    action_firstLoop = false;
                }
                break;

            case ENUM_current_state.working:

                agent.SetDestination(playerPos);
                //gameObject.transform.position = playerPos - new Vector3(0.1f,0.1f);
                

                if(Vector3.Distance(transform.position, playerPos) <= 1)
                {
                    if(action_firstLoop == false)
                    {
                        anim.SetTrigger("isLanding");
                        action_firstLoop = true;
                    }
                }
                break;

                // Ustawione z animacji
            case ENUM_current_state.finishing:
                specialEffects.PlayJumpSlam();
                RaycastHit2D[] hits = Physics2D.CircleCastAll(playerPos, fallAreaRadius, new Vector2(0.5f, 0.5f));
                //Debug.Log("hits count = " + hits.Length);
                if(hits != null)
                {
                    foreach(RaycastHit2D raycast in hits)
                    {
                        if(raycast.collider.gameObject.CompareTag("Player"))
                        {
                            Vector2 dir = (raycast.collider.gameObject.transform.position - transform.position).normalized;
                            Player player = raycast.collider.GetComponent<Player>();
                            player.controller.rb.AddForce(dir * knockBackForce);
                            player.TakeDamage(jumpDamage);

                            //Debug.Log("name = " + raycast.collider.gameObject.name);
                        }
                    }
                }
                //Debug.LogWarning("SCREEN SHAKE");
                action_firstLoop = true;
                //Debug.Log("END Jump State");
                last_action = 2;
                currentActionState = ENUM_current_state.ready_to_exit;
                break;

            case ENUM_current_state.ready_to_exit:
                break;

        }
    }
    void Action_Rushing()
    {
        switch (currentActionState)
        {
            case ENUM_current_state.preparation:
                if(action_firstLoop == true)
                {
                    //Debug.Log("Rush State");
                    anim.SetTrigger("isRushing");
                    action_firstLoop = false;
                }
                break;
            case ENUM_current_state.working:
                playerPos = move_target.transform.position;
                agent.SetDestination(playerPos);
                agent.speed *= speedModifier;
                agent.acceleration *= speedModifier;
                is_rushing = true;
                currentActionState = ENUM_current_state.finishing;
                break;
            case ENUM_current_state.finishing:
                if(Vector3.Distance(playerPos, transform.position) <= 1)
                {
                    agent.speed /= speedModifier;
                    agent.acceleration /= speedModifier;
                    agent.SetDestination(transform.position);
                    is_rushing = false;
                    agent.velocity = Vector3.zero;
                    currentActionState = ENUM_current_state.ready_to_exit;
                    //Debug.Log("END Rush State");
                    last_action = 1;
                }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(is_rushing)
        {
            if(collision.CompareTag("Player"))
            {
                Vector3 dir = (collision.gameObject.transform.position - transform.position).normalized;
                Player player = collision.GetComponent<Player>();
                player.controller.rb.AddForce(dir * knockBackForce);
                player.TakeDamage(rushDamage);
            }
        }
    }

    public override void MeleeAttack_Action()
    {
        // UNUSED
    }

    public override void RangedAttack_Action()
    {
        // UNUSED
    }

    
}
