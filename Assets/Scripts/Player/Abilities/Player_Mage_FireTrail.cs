using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Mage_FireTrail : Ability_1
{
    [Space(10)]
    public float moveSpeedIncrease = 150;
    public float durationTime = 3;
    [SerializeField] float trailDamage = 2;
    bool isWalking;
    float trailTimer;
    public GameObject trailObject;

    void Start()
    {
        rechargeTime = 10;
        energyCost = 10;
    }

    public override void Use()
    {
        FireTrail();
    }

    void FireTrail()
    {
        if(isWalking == false && isRecharching == false && player.stats.currentEnergy >= energyCost)
        {
            player.controller.moveSpeed += moveSpeedIncrease;
            player.stats.ModifyEnergy(-energyCost);
            isWalking = true;

        }
    }

    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if (Game_State.gamePaused)
            return;
        
        if(isRecharching)
        {
            timer += Time.deltaTime;
            if(timer >= rechargeTime)
            {
                isRecharching = false;
                timer = 0;
                trailTimer = 0;
                player.ui_updater.Ability1Recharged();
            }
        }
        if(isWalking)
        {
            timer += Time.deltaTime;
            trailTimer += Time.deltaTime;
            if (timer >= durationTime)
            {
                isRecharching = true;
                isWalking = false;
                timer = 0;
                trailTimer = 0;
                player.controller.moveSpeed -= moveSpeedIncrease;
                player.ui_updater.Ability1Used();
            }

            if(trailTimer >= 0.5)
            {
                 
                GameObject temp = Instantiate(trailObject);
                Player_FireTrailObject fire = temp.GetComponent<Player_FireTrailObject>();
                fire.damage = trailDamage;
                temp.transform.position = transform.position;
                trailTimer -= 0.5f;
            }
        }
    }
}
