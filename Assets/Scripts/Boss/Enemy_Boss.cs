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
public enum ENUM_current_state
{
    preparation,
    working,
    finishing,
    ready_to_exit
}


public class Enemy_Boss : Enemy_BaseClass
{
    [Space(20)]
    [SerializeField] ENUM_BossState boss_state;
    [SerializeField] ENUM_current_state current_action_state; 
    bool can_move;

    [Header("Dash Action")]
    [SerializeField] float dash_force;
    [SerializeField] float dash_recharge_time;
    float dash_timer;
    bool dash_WaitingForRecharge;

    [Header("Idle Action")]
    float idle_timer;
    float idle_time;
    [SerializeField] Vector4 arena_bounds;

    [Header("Jump Action")]
    [SerializeField] Vector3 player_pos;
    [SerializeField] float fall_area_radius;
    [SerializeField] float knockBackForce;

    [Header("Rush Aciton")]
    [SerializeField] float speed_modifier;
    bool is_rushing;
    

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

    }
    void Start()
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

    // ====================================================================== //



    void Update()
    {
        if(move_target == null)
        {
            return;
        }

        DashRefreshing();
        CheckDistanceToPlayer();

        // Check if duck should dash away from player
        if (distance_To_Player <= 2)
        {
            if(dash_WaitingForRecharge == false)
            {
                if(current_action_state == ENUM_current_state.ready_to_exit)
                {
                    boss_state = ENUM_BossState.dash;
                    current_action_state = ENUM_current_state.preparation;
                    return;
                }
            }
        }

        // Decide randomly, which action should duck choose
        if(current_action_state == ENUM_current_state.ready_to_exit)
        {
            int rand = Random.Range(0, 4);
            switch (rand)
            {
                case 0:
                    boss_state = ENUM_BossState.idle;
                    current_action_state = ENUM_current_state.preparation;
                    break;
                case 1:
                    boss_state = ENUM_BossState.attack_rushing;
                    current_action_state = ENUM_current_state.preparation;
                    break;
                case 2:
                    boss_state = ENUM_BossState.attack_jumping;
                    current_action_state = ENUM_current_state.preparation;
                    break;
                case 3:
                    boss_state = ENUM_BossState.moving;
                    current_action_state = ENUM_current_state.preparation;
                    break;
                default:
                    break;
            }
        }

        // Do current state              *Quack *Quack
        switch (boss_state)
        {
            case ENUM_BossState.unseen:
                action_Unseen();
                break;
            case ENUM_BossState.idle:
                action_Idle();
                break;
            case ENUM_BossState.moving:
                action_Moving();
                break;
            case ENUM_BossState.dash:
                action_Dash();
                break;
            case ENUM_BossState.attack_jumping:
                action_Jumping();
                break;
            case ENUM_BossState.attack_rushing:
                action_Rushing();
                break;
            case ENUM_BossState.dying:
                action_Dying();
                break;
            default:
                Debug.LogError("ERROR. UKNOWN BOSS STATE");
                break;
        }

    }

    public void EVENT_Preparation()
    {
        current_action_state = ENUM_current_state.preparation;
    }
    public void EVENT_Working()
    {
        current_action_state = ENUM_current_state.working;
    }
    public void EVENT_Finishing()
    {
        current_action_state = ENUM_current_state.finishing;
    }
    public void EVENT_Exiting()
    {
        current_action_state = ENUM_current_state.ready_to_exit;
    }

    void DashRefreshing()
    {
        if(dash_WaitingForRecharge == true)
        {
            dash_timer += Time.deltaTime;
            if(dash_timer >= dash_recharge_time)
            {
                dash_timer = 0;
                dash_WaitingForRecharge = false;
            }

        }
    }

    void action_Unseen()
    {

    }
    void action_Idle()
    {
        switch (current_action_state)
        {
            case ENUM_current_state.preparation:
                idle_time = Random.Range(2, 5);
                idle_timer = 0;
                can_move = false;
                current_action_state = ENUM_current_state.working;
                anim.SetTrigger("Idle");
                break;

            case ENUM_current_state.working:
                idle_timer += Time.deltaTime;

                if(idle_timer >= idle_time)
                {
                    current_action_state = ENUM_current_state.finishing;
                }
                break;

            case ENUM_current_state.finishing:
                can_move = true;
                current_action_state = ENUM_current_state.ready_to_exit;
                break;

            case ENUM_current_state.ready_to_exit:
                break;
            default:
                Debug.LogError("ERROR. UNKOWN ACTION STATE");
                    break;
        }
        
    }
    void action_Moving()
    {
        switch (current_action_state)
        {
            case ENUM_current_state.preparation:
                Vector3 random_spot = new Vector3(Random.Range(arena_bounds.x, arena_bounds.y), Random.Range(arena_bounds.z, arena_bounds.w),transform.position.z);
                agent.SetDestination(random_spot);
                current_action_state = ENUM_current_state.working;
                anim.SetTrigger("Moving");
                break;
            case ENUM_current_state.working:
                //Debug.Log("agent dest = " + agent.destination);

                if(Vector3.Distance(gameObject.transform.position, agent.destination) < 1.5f)
                {
                    current_action_state = ENUM_current_state.finishing;
                }
                break;
            case ENUM_current_state.finishing:
                current_action_state = ENUM_current_state.ready_to_exit;
                break;
            case ENUM_current_state.ready_to_exit:
                break;
                
        }
        
    }
    void action_Dash()
    {
        switch (current_action_state)
        {
            case ENUM_current_state.preparation:
                Vector2 direction_to_player = (move_target.transform.position - transform.position).normalized;
                rb.AddForce(-direction_to_player * dash_force);
                can_move = false;
                current_action_state = ENUM_current_state.finishing;
                break;
            case ENUM_current_state.working:
                break;
            case ENUM_current_state.finishing:
                can_move = true;
                dash_WaitingForRecharge = true;
                current_action_state = ENUM_current_state.ready_to_exit;
                break;
            case ENUM_current_state.ready_to_exit:
                break;
            default:
                Debug.LogError("ERROR. UNKOWN ACTION STATE");
                break;
        }
    }
    void action_Jumping()
    {
        switch (current_action_state)
        {
            case ENUM_current_state.preparation:
                player_pos = move_target.position;
                anim.SetTrigger("JumpAttack");
                can_move = false;
                
                // State changed via Animation
                break;

            case ENUM_current_state.working:
                gameObject.transform.position = player_pos - new Vector3(0.1f,0.1f);
                


                break;

            case ENUM_current_state.finishing:
                RaycastHit2D[] hits = Physics2D.CircleCastAll(player_pos, fall_area_radius, new Vector2(0.5f, 0.5f));
                //Debug.Log("hits count = " + hits.Length);
                if(hits != null)
                {
                    foreach(RaycastHit2D raycast in hits)
                    {
                        if(raycast.collider.gameObject.CompareTag("Player"))
                        {
                            Vector2 dir = (raycast.collider.gameObject.transform.position - transform.position).normalized;
                            raycast.collider.GetComponent<Player>().controller.rb.AddForce(dir * knockBackForce);
                            //Debug.Log("name = " + raycast.collider.gameObject.name);
                        }
                    }
                }
                current_action_state = ENUM_current_state.ready_to_exit;
                break;

            case ENUM_current_state.ready_to_exit:
                break;

        }
    }
    void action_Rushing()
    {
        switch (current_action_state)
        {
            case ENUM_current_state.preparation:
                player_pos = move_target.transform.position;
                anim.SetTrigger("RushAttack");
                break;
            case ENUM_current_state.working:
                agent.SetDestination(player_pos);
                agent.speed *= speed_modifier;
                agent.acceleration *= speed_modifier;
                is_rushing = true;
                current_action_state = ENUM_current_state.finishing;
                break;
            case ENUM_current_state.finishing:
                if(Vector3.Distance(player_pos, transform.position) <= 1)
                {
                    agent.speed /= speed_modifier;
                    agent.acceleration /= speed_modifier;
                    current_action_state = ENUM_current_state.ready_to_exit;
                    is_rushing = false;
                }
                break;
            case ENUM_current_state.ready_to_exit:
                break;

        }
    }
    void action_Dying()
    {
        switch (current_action_state)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(is_rushing)
        {
            if(collision.CompareTag("Player"))
            {
                Vector3 dir = (collision.gameObject.transform.position - transform.position).normalized;
                collision.GetComponent<Player>().controller.rb.AddForce(dir * knockBackForce);
            }
        }
    }


}
