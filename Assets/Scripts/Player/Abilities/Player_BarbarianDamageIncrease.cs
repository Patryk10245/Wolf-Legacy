using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_BarbarianDamageIncrease : Ability_2
{
    public float damageMultiplier = 4;
    public float basePlayerDamage;
    public float durationTime;
    public bool isIncreased;
    public override void Use()
    {
        if(player.stats.currentEnergy >= energyCost && isRecharching == false)
        {
            player.stats.ModifyEnergy(-energyCost);
            IncreaseDamage();
        }
    }

    private void Start()
    {
        energyCost = 15;
        rechargeTime = 20;
        durationTime = 8;
    }

    public void IncreaseDamage()
    {
        isIncreased = true;
        player.stats.damage *= damageMultiplier;
    }
    private void Update()
    {
        if(isIncreased == true)
        {
            timer += Time.deltaTime;
            if (timer >= durationTime)
            {
                timer = 0;
                isRecharching = true;
                player.stats.damage = basePlayerDamage;
                isIncreased = false;
            }
        }
        if(isRecharching == true)
        {
            timer += Time.deltaTime;
            if(timer >= rechargeTime)
            {
                timer = 0;
                isRecharching = false;
            }
        }
    }
}
