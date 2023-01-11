using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ArcherDash : Ability_1
{
    public float dashForce = 6000;
    public float energyCost = 8;
    public float rechargeTime = 4;
    float timer;


    bool isRecharching;


    public override void Use()
    {
        if(isRecharching == false & player.stats.currentEnergy >= energyCost)
        {
            Dash();
        }
    }
    private void Update()
    {
        if (isRecharching)
        {
            timer += Time.deltaTime;
            if (timer >= rechargeTime)
            {
                isRecharching = false;
                timer = 0;
            }
        }
    }
    void Dash()
    {
        Vector3 mousepos = player.controller.mousePos;
        mousepos = Camera.main.ScreenToWorldPoint(mousepos);
        Vector3 dir = (transform.position - mousepos).normalized;

        player.controller.rb.AddForce(dir * dashForce);
        isRecharching = true;
    }


}
