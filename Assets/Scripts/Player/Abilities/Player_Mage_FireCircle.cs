using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Mage_FireCircle : Ability_2
{
    [Space(10)]
    public float damageMultiplier = 3f;
    public float circleDamage = 1;

    public float explosiveCircleRange = 2.5f;

    private void Start()
    {
        rechargeTime = 8;
        energyCost = 15;
    }

    public override void Use()
    {
        CastCircle();
    }
    void CastCircle()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, explosiveCircleRange, new Vector2(0.5f, 0.5f));
        if (hits != null)
        {
            foreach (RaycastHit2D raycast in hits)
            {
                if (raycast.collider.gameObject.CompareTag("Enemy"))
                {
                    Enemy_BaseClass enemy = raycast.collider.gameObject.GetComponent<Enemy_BaseClass>();
                    enemy.TakeDamage(circleDamage, ENUM_AttackType.ranged, player);
                }
                if (raycast.collider.gameObject.CompareTag("Boss"))
                {
                    Enemy_BaseClass enemy = raycast.collider.gameObject.GetComponent<Enemy_BaseClass>();
                    enemy.TakeDamage(circleDamage, ENUM_AttackType.ranged, player);
                }
                if (raycast.collider.gameObject.CompareTag("Spawner"))
                {
                    Enemy_Spawner enemy = raycast.collider.gameObject.GetComponent<Enemy_Spawner>();
                    enemy.TakeDamage(circleDamage, ENUM_AttackType.ranged);
                }
            }
        }
        isRecharching = true;


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
    }
}
