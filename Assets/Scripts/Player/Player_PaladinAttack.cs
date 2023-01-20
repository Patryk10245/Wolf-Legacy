using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_PaladinAttack : Player_AttackScript
{
    public override void Attack()
    {
        player.inAttack = true;
        player.controller.weaponAnimator.SetTrigger("isClicked");
        player.controller.weaponCollider.enabled = true;
        player.controller.trailObject.SetActive(true);
    }

    public override void CreateProjectile()
    {
        throw new System.NotImplementedException();
    }
}
