using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MageAttack : Player_AttackScript
{
    public GameObject projectilePrefab;
    public Transform spawnProjectilePosition;

    public Vector3 mousepos;
    public Vector3 dir;
    public Vector3 directionNormalized;

    [SerializeField] float projectileSpeed = 800;
        
    public override void Attack()
    {
        player.controller.weaponAnimator.SetTrigger("Attack");
    }

    public override void CreateProjectile()
    {
        GameObject temp = Instantiate(projectilePrefab);
        temp.transform.position = spawnProjectilePosition.position;

        mousepos = player.controller.mousePos;
        mousepos.z = Camera.main.nearClipPlane;
        mousepos = player.currentCamera.WorldToScreenPoint(transform.localPosition);

        dir = (mousepos - spawnProjectilePosition.transform.position);

        dir = (Vector3)player.controller.mousePos - player.controller.screenPoint;
        dir.z = 0;
        if(player.controller.mousePos == Vector2.zero)
        {
            Debug.Log("it is 0");
            Debug.Log("Scale = " + transform.localScale);
            dir = Vector3.zero;
            dir.x = transform.localPosition.x;
        }
        else
        {
            Debug.Log(player.controller.mousePos);
            Debug.Log("Scale = " + transform.localScale);
        }

        directionNormalized = Vector3.Normalize(dir);

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
