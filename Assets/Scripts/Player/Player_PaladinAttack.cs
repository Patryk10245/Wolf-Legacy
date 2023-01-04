using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_PaladinAttack : Player_AttackScript
{
    public override void Attack()
    {
        player.inAttack = true;
        player.controller.swordAnimator.SetTrigger("isClicked");
        player.controller.swordCollider.enabled = true;
        player.controller.trailObject.SetActive(true);
    }
}
