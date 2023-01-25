using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Barbarian_Leap : Ability_1
{
    public float leapForce = 1000;
    public float leapTime = 0.5f;
    public bool isLeaping;
    public float leapDamage;
    public float damageMultiplier = 3f;


    public Vector3 direction;

    private void Start()
    {
        energyCost = 10;
        rechargeTime = 4;

    }

    private void Update()
    {
        if(isLeaping)
        {
            player.controller.rb.AddForce(-direction * leapForce);

            timer += Time.deltaTime;
            if (timer >= leapTime)
            {
                isLeaping = false;
                timer = 0;
            }
        }
    }
    public override void Use()
    {
        if(isRecharching == false && player.stats.currentEnergy >= energyCost)
        {
            player.stats.ModifyEnergy(-energyCost);
            leapDamage = player.stats.damage * damageMultiplier;
            Leap();
        }
    }

    void Leap()
    {
        isLeaping = true;
        Vector3 mousepos = player.controller.mousePos;
        mousepos.z = Camera.main.nearClipPlane;
        mousepos = Camera.main.ScreenToWorldPoint(mousepos);
        mousepos.z = transform.position.z;
        direction = (transform.position - mousepos).normalized;
        player.controller.animBody.SetTrigger("barbarianLeap");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isLeaping == true)
        {
            if(collision.gameObject.CompareTag("Enemy")|| collision.gameObject.CompareTag("Boss"))
            {
                collision.gameObject.GetComponent<Enemy_BaseClass>().TakeDamage(leapDamage, ENUM_AttackType.melee, player);
            }
        }
    }


}