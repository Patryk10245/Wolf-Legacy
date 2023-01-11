using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Paladin_DashSkill : Ability_1
{
    [Space(10)]
    public float dashForce = 4000;
    public float dashTime = 0.1f;
    bool isDashing;

    private void Start()
    {
        rechargeTime = 3;
        energyCost = 10;
    }

    private void Update()
    {
        if(isRecharching == true)
        {
            timer += Time.deltaTime;
            if(timer >= rechargeTime)
            {
                timer = 0;
                isRecharching = false;
                player.ui_updater.Ability1Recharged();
            }
        }

        if(isDashing)
        {
            timer += Time.deltaTime;
            if(timer >= dashTime)
            {
                timer = 0;
                isDashing = false;
                player.KnockBack(player.controller.moveInput * dashForce);
                isRecharching = true;
                player.ui_updater.Ability1Used();
            }
        }
    }
    void Dash()
    {
        if(player.stats.currentEnergy >= energyCost && isRecharching == false && isDashing == false)
        {
            player.stats.ModifyEnergy(-energyCost);
            //Debug.Log("Force = " + force);
            player.KnockBack(player.controller.moveInput * dashForce);
            isDashing = true;
            timer = 0;
        }
    }

    public override void Use()
    {
        Dash();
    }
}
