using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Barbarian_Leap : Ability_1
{
    public float leapForce = 9000;
    public bool isLeaping;

    private void Start()
    {
        energyCost = 10;
        rechargeTime = 4;

    }

    public override void Use()
    {
        if(isRecharching == false && player.stats.currentEnergy >= energyCost)
        {
            Leap();
        }
    }

    void Leap()
    {
        Vector3 force = player.controller.mousePos.normalized * leapForce;
        isLeaping = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isLeaping == true)
        {

        }
    }


}
