using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Mage_FireCircle : Ability_2
{
    public float rechargeTime = 10;
    public float energyCost = 10;
    public float durationTime = 3;
    [SerializeField] float circleDamage = 2;
    bool isRecharching;
    bool isBurning;
    float timer;

    public GameObject circlePrefab;

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
            }
        }
        
    }
}