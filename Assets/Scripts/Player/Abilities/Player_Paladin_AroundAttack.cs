using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Paladin_AroundAttack : Ability_2
{
    public GameObject weaponHolder;

    public float rechargeTime = 5;
    public float rotationTime = 0.26f;
    public Vector3 rotationSpeed = new Vector3(0,0,-1500);
    public Vector3 weaponScale = new Vector3(1.5f,1,1);

    public float energyCost = 15;

    float timer;
    bool isRecharching;
    bool isUsed;
    public override void Use()
    {
        if (player.stats.currentEnergy >= energyCost && isRecharching == false && isUsed == false)
        {
            player.stats.ModifyEnergy(-energyCost);
            isUsed = true;
            timer = 0;
            player.controller.weaponCollider.enabled = true;
        }
    }
    private void Update()
    {
        if(isUsed)
        {
            weaponHolder.transform.Rotate(rotationSpeed * Time.deltaTime);
            timer += Time.deltaTime;
            weaponHolder.transform.localScale = weaponScale;
            player.canRotateWeapon = false;

            if (timer >= rotationTime)
            {
                timer = 0;
                isRecharching = true;
                isUsed = false;
                player.canRotateWeapon = true;
                player.controller.weaponCollider.enabled = false;
                weaponHolder.transform.rotation.eulerAngles.Set(0, 0, 0);
                weaponHolder.transform.localScale = Vector3.one;
            }
        }

        if(isRecharching)
        {
            timer += Time.deltaTime;
            if(timer >= rechargeTime)
            {
                isRecharching = false;
                timer = 0;
            }
        }
    }


}
