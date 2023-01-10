using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_PaladinAttack : Player_AttackScript
{
    public override void Attack()
    {
        //Debug.Log("object = " + gameObject.name);
        player.inAttack = true;
        //Debug.Log("player object = " + player.name);
        player.controller.weaponAnimator.SetTrigger("isClicked");
        player.controller.weaponCollider.enabled = true;
        player.controller.trailObject.SetActive(true);
    }

    public override void CreateProjectile()
    {
        throw new System.NotImplementedException();
    }
}
