using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DashSkill : MonoBehaviour
{
    public Player player;
    public float dashForce;
    public float dashRechargeTime;
    public float dashEnergyCost;
    float dash_timer;

    private void Update()
    {
        if(Input.GetKeyDown("l"))
        {
            Dash();
        }
    }
    void Dash()
    {
        if(player.stats.currentEnergy >= dashEnergyCost)
        {
            player.stats.ModifyEnergy(-dashEnergyCost);
            player.KnockBack(player.controller.moveInput * dashForce);
        }
    }

}
