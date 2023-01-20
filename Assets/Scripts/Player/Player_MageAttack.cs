using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MageAttack : Player_AttackScript
{
    public GameObject projectilePrefab;
    public Transform spawnProjectilePosition;

    [SerializeField] float projectileSpeed = 800;
        
    public override void Attack()
    {
        player.controller.weaponAnimator.SetTrigger("Attack");
    }

    public override void CreateProjectile()
    {
        GameObject temp = Instantiate(projectilePrefab);
        temp.transform.position = spawnProjectilePosition.position;

        Vector3 mousepos = player.controller.mousePos;
        mousepos.z = Camera.main.nearClipPlane;
        mousepos = Camera.main.ScreenToWorldPoint(mousepos);
        
        Vector3 dir = (mousepos - spawnProjectilePosition.transform.position);
        dir.z = 0;

        Vector3 directionNormalized = Vector3.Normalize(dir);

        Player_Projectile projectile = temp.GetComponent<Player_Projectile>();
        projectile.rb.AddForce(directionNormalized * projectileSpeed);
        projectile.flyDirection = directionNormalized;
        projectile.speed = projectileSpeed;
        projectile.damage = player.stats.damage;
        projectile.stopTimerAt = 4;
        projectile.player = player;
        AudioManager.ins.Play_MageFireball();
    }

}
