using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DashSkill : Ability_1
{
    public Player player;
    public float dashForce = 4000;
    public float dashRechargeTime = 3;
    public float dashEnergyCost = 10;
    float dash_timer;
    public float dashTime = 0.1f;
    bool isDashing;
    bool isRecharching;

    private void Update()
    {
        if(isRecharching == true)
        {
            dash_timer += Time.deltaTime;
            if(dash_timer >= dashRechargeTime)
            {
                dash_timer = 0;
                isRecharching = false;
            }
        }

        if(isDashing)
        {
            dash_timer += Time.deltaTime;
            if(dash_timer >= dashTime)
            {
                dash_timer = 0;
                isDashing = false;
                player.KnockBack(player.controller.moveInput * dashForce);
                isRecharching = true;
            }
        }
    }
    void Dash()
    {
        if(player.stats.currentEnergy >= dashEnergyCost && isRecharching == false && isDashing == false)
        {
            player.stats.ModifyEnergy(-dashEnergyCost);
            //Debug.Log("Force = " + force);
            player.KnockBack(player.controller.moveInput * dashForce);
            isDashing = true;
            dash_timer = 0;
        }
    }

    public override void Use()
    {
        Dash();
    }
}
