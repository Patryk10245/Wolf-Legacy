using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_RangerAttack : Player_AttackScript
{
    public GameObject projectilePrefab;
    public Transform spawnProjectilePosition;
    [SerializeField] float projectileSpeed = 1000;

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

        dir = (Vector3)player.controller.mousePos - player.controller.screenPoint;
        dir.z = 0;
        if (player.controller.mousePos == Vector2.zero)
        {
            dir = Vector3.zero;
            dir.x = transform.localScale.x;
        }

        Vector3 directionNormalized = Vector3.Normalize(dir);

        Player_Projectile projectile = temp.GetComponent<Player_Projectile>();
        projectile.rb.AddForce(directionNormalized * projectileSpeed);
        projectile.flyDirection = directionNormalized;
        projectile.speed = projectileSpeed;
        projectile.damage = player.stats.damage;
        projectile.stopTimerAt = 4;
        projectile.player = player;
    }
}
