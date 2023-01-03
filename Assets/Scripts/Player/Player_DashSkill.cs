using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DashSkill : Ability_1
{
    public Player player;
    public float dashForce = 500;
    public float dashRechargeTime = 5;
    public float dashEnergyCost = 1;
    float dash_timer;
    public float dashTime = 0.1f;
    bool isDashing;

    private void Update()
    {
        if(isDashing)
        {
            dash_timer += Time.deltaTime;
            if(dash_timer >= dashTime)
            {
                dash_timer = 0;
                isDashing = false;
                player.KnockBack(player.controller.moveInput * dashForce);
            }
        }
    }
    void Dash()
    {
        if(player.stats.currentEnergy >= dashEnergyCost)
        {
            player.stats.ModifyEnergy(-dashEnergyCost);
            Vector3 force = new Vector3(player.controller.moveInput.x, player.controller.moveInput.y, 0);
            //Debug.Log("Force = " + force);
            player.KnockBack(player.controller.moveInput * dashForce);
            isDashing = true;
        }
    }

    public override void Use()
    {
        Dash();
    }
}
