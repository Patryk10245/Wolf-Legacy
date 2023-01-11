using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ArcherMultiShot : Ability_2
{
    public float damage = 4;
    public float arrowSpeed = 8;
    public float rechargeTime = 6;
    float timer;
    public float timeBetweenArrows = 0.1f;
    public float energyCost = 10;

    public int arrowsToShot = 6;
    int arrowsAlreadyShot;
    public GameObject arrowPrefab;
    public GameObject arrowSpawnPosiiton;

    bool isRecharching;
    bool isShooting;

    public override void Use()
    {
        if (isRecharching == false && player.stats.currentEnergy >= energyCost)
        {
            MultiShot();
        }
    }

    // Update is called once per frame
    void Update()
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

        if(isShooting)
        {
            timer += Time.deltaTime;

            if(timer >= timeBetweenArrows)
            {
                timer -= timeBetweenArrows;
                arrowsAlreadyShot++;
                GameObject arrow = Instantiate(arrowPrefab);
                arrow.transform.position = arrowSpawnPosiiton.transform.position;
                Player_Projectile proj = arrow.GetComponent<Player_Projectile>();

                Vector3 mousepos = player.controller.mousePos;
                mousepos = Camera.main.ScreenToWorldPoint(mousepos);
                Vector3 dir = (mousepos - arrowSpawnPosiiton.transform.position).normalized;

                proj.flyDirection = dir;
                proj.speed = arrowSpeed;
                proj.damage = damage;
                proj.stopTimerAt = 4;
            }

            if(arrowsAlreadyShot >= arrowsToShot)
            {
                arrowsAlreadyShot = 0;
                timer = 0;
                isShooting = false;
                isRecharching = true;
            }
        }
    }
    void MultiShot()
    {
        isShooting = true;
        timer = 0;
        arrowsAlreadyShot = 0;
    }
}
