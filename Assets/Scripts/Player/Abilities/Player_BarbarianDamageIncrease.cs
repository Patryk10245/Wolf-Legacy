using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_BarbarianDamageIncrease : Ability_2
{
    public float damageMultiplier = 2;
    public float basePlayerDamage;
    public float durationTime = 5;
    public bool isIncreased;

    public Color bodyColor = new Color(1f, 0.65f, 0.65f);
    public Color weaponColor = new Color(0.85f, 0.25f, 0.25f);

    SpriteRenderer bodySprite;
    SpriteRenderer weaponSprite;


    public override void Use()
    {
        if(player.stats.currentEnergy >= energyCost && isRecharching == false)
        {
            player.stats.ModifyEnergy(-energyCost);
            IncreaseDamage();
            player.ui_updater.Ability2Used();
            //player.controller.animBody.SetTrigger("damageIncrease");
            if(bodySprite == null)
            {
                foreach(Transform child in player.transform)
                {
                    if (child.name == "Player Body")
                    {
                        bodySprite = child.GetComponent<SpriteRenderer>();
                        
                    }
                }
                weaponSprite = player.controller.weaponCollider.gameObject.GetComponent<SpriteRenderer>();
            }
            bodySprite.color = bodyColor;
            weaponSprite.color = weaponColor;
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
                bodySprite.color = Color.white;
                weaponSprite.color = Color.white;
            }
        }
        if(isRecharching == true)
        {
            timer += Time.deltaTime;
            if(timer >= rechargeTime)
            {
                timer = 0;
                isRecharching = false;
                player.ui_updater.Ability2Recharged();
                
            }
        }
    }
}
