using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Mage_FireCircle : Ability_2
{
    [Space(10)]
    public float damageMultiplier = 0.01f;
    public float durationTime = 3;
    public float circleDamage = 1;
    bool isBurning;

    public GameObject circlePrefab;

    private void Start()
    {
        rechargeTime = 10;
        energyCost = 10;
    }

    public override void Use()
    {
        FireCircle();
    }

    void FireCircle()
    {
        if(isBurning == false && isRecharching == false && player.stats.currentEnergy >= energyCost)
        {
            CastCircle();
        }
    }
    void CastCircle()
    {
        player.isInvulnerable = true;
        isBurning = true;
        GameObject temp = Instantiate(circlePrefab);
        Debug.Log(temp.name);
        temp.transform.position = transform.position;

        Player_FireCircleObject firecircle = temp.GetComponent<Player_FireCircleObject>();
        firecircle.expirationTime = durationTime;
        firecircle.damage = circleDamage;

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
                player.ui_updater.Ability2Recharged();
            }
        }

        if(isBurning)
        {
            timer += Time.deltaTime;
            if (timer >= durationTime)
            {
                timer = 0;
                isBurning = false;
                isRecharching = true;
                player.isInvulnerable = false;
                player.ui_updater.Ability2Used();
            }
        }
        
    }
}
