using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum ENUM_BossState
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
public class Enemy_Boss : Enemy_BaseClass
{
    [Space(20)]
    [SerializeField] ENUM_BossState bossState;
    int last_action;
    [SerializeField] ENUM_current_state currentActionState = ENUM_current_state.ready_to_exit;
    bool action_firstLoop = true;
    bool can_move;
    [SerializeField] float changeTargetTime = 15;
    float change_target_timer;

    [Header("Dash Action")]
    [SerializeField] float dashForce;
    [SerializeField] float dashRechargeTime;
    float dash_timer;
    bool dash_WaitingForRecharge;

    [Header("Idle Action")]
    float idle_timer;
    float idle_time;
    [SerializeField] Vector4 arenaBounds;

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


    void Update()
    {
        if(move_target == null)
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
                    bossState = ENUM_BossState.dash;
                    currentActionState = ENUM_current_state.preparation;
                    return;
                }
            }
        }

        // Decide randomly, which action should duck choose
        if(currentActionState == ENUM_current_state.ready_to_exit)
        {
            int rand;
            do
            {
                rand = Random.Range(0, 4);
                Debug.Log("last = " + last_action + "| current = " + rand);

                switch (rand)
                {
                    case 0:
                        bossState = ENUM_BossState.idle;
                        currentActionState = ENUM_current_state.preparation;
                        break;
                    case 1:
                        bossState = ENUM_BossState.attack_rushing;
                        currentActionState = ENUM_current_state.preparation;
                        break;
                    case 2:
                        bossState = ENUM_BossState.attack_jumping;
                        currentActionState = ENUM_current_state.preparation;
                        break;
                    case 3:
                        bossState = ENUM_BossState.moving;
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
            case ENUM_BossState.unseen:
                Action_Unseen();
                break;
            case ENUM_BossState.idle:
                Action_Idle();
                break;
            case ENUM_BossState.moving:
                Action_Moving();
                break;
            case ENUM_BossState.dash:
                Action_Dash();
                break;
            case ENUM_BossState.attack_jumping:
                Action_Jumping();
                break;
            case ENUM_BossState.attack_rushing:
                Action_Rushing();
                break;
            case ENUM_BossState.dying:
                Action_Dying();
                break;
            default:
                Debug.LogError("ERROR. UKNOWN BOSS STATE");
                break;
        }

        

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

    public void EVENT_Preparation()
    {
        Debug.Log("Event Preparation");
        currentActionState = ENUM_current_state.preparation;
    }
    public void EVENT_Working()
    {
        Debug.Log("Event Working");
        currentActionState = ENUM_current_state.working;
    }
    public void EVENT_Finishing()
    {
        Debug.Log("Event Finishing");
        currentActionState = ENUM_current_state.finishing;
    }
    public void EVENT_Exiting()
    {
        Debug.Log("Event Exiting");
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
                idle_time = Random.Range(2, 5);
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
                Vector3 random_spot = new Vector3(Random.Range(arenaBounds.x, arenaBounds.y), Random.Range(arenaBounds.z, arenaBounds.w),transform.position.z);
                //Debug.Log("random spot = " + random_spot);
                agent.SetDestination(random_spot);
                anim.SetTrigger("isMoving");
                currentActionState = ENUM_current_state.working;
                break;
            case ENUM_current_state.working:
                //Debug.Log("agent dest = " + agent.destination);

                if(Vector3.Distance(gameObject.transform.position, agent.destination) < 1.5f)
                {
                    currentActionState = ENUM_current_state.finishing;
                }
                break;
            case ENUM_current_state.finishing:
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
                Debug.Log("Dash State");
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
                agent.velocity = Vector3.zero;
                currentActionState = ENUM_current_state.ready_to_exit;
                Debug.Log("END Dash State");
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
                //Debug.Log("Rush State");
                playerPos = move_target.transform.position;
                anim.SetTrigger("isRushing");
                currentActionState = ENUM_current_state.working;
                break;
            case ENUM_current_state.working:
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
        ChangeState(ENUM_EnemyState.dying);
        is_dying = true;
         


        switch (currentActionState)
        {
            case ENUM_current_state.preparation:
                currentActionState = ENUM_current_state.finishing;
                break;
            case ENUM_current_state.working:
                break;
            case ENUM_current_state.finishing:
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
