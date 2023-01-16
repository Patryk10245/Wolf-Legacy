using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_RangerAttack : Player_AttackScript
{
    public GameObject projectilePrefab;
    public Transform spawnProjectilePosition;
    [SerializeField] float projectileSpeed = 10;

    public override void Attack()
    {
        //Debug.Log("Mage Attack");
        player.controller.weaponAnimator.SetTrigger("Attack");
    }

    public override void CreateProjectile()
    {
        GameObject temp = Instantiate(projectilePrefab);
        temp.transform.position = spawnProjectilePosition.position;
        Vector3 mousepos = player.controller.mousePos;
        mousepos = Camera.main.ScreenToWorldPoint(mousepos);
        Vector3 dir = (mousepos - spawnProjectilePosition.transform.position).normalized;
        //Debug.Log("Dir = " + dir);

        Player_Projectile projectile = temp.GetComponent<Player_Projectile>();
        projectile.flyDirection = dir;
        projectile.speed = projectileSpeed;
        projectile.damage = player.stats.damage;
        projectile.stopTimerAt = 4;
        projectile.player = player;
    }
}
