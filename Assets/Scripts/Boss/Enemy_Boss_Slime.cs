using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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

    public ENUM_DuckBossState bossState;
    public ENUM_current_state currentActionState = ENUM_current_state.ready_to_exit;
    int last_action;



    [Header("Idle Action")]
    [SerializeField] float minIdleTime = 1;
    [SerializeField] float maxIdleTime = 3;
    float idle_timer;
    float idle_time;
    [SerializeField] Vector4 arenaBounds;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}

