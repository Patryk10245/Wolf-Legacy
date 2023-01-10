using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Mage_FireTrail : Ability_1
{
    public float moveSpeedIncrease = 150;
    public float rechargeTime = 10;
    public float energyCost = 10;
    public float durationTime = 3;
    [SerializeField] float trailDamage = 2;
    bool isWalking;
    bool isRecharching;
    float timer;



    float trailTimer;
    public GameObject trailObject;

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
    void Start()
    {
        
    }

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
            }
        }
        if(isWalking)
        {
            timer += Time.deltaTime;
            trailTimer += Time.deltaTime;
            if (timer >= durationTime)
            {
                isWalking = false;
                timer = 0;
                trailTimer = 0;
                player.controller.moveSpeed -= moveSpeedIncrease;
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
