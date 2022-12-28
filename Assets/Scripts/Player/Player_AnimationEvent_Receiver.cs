using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AnimationEvent_Receiver : MonoBehaviour
{
    public Player player;
    public void EVENT_AttackAnimationEnd()
    {
        player.inAttack = false;
        player.controller.swordCollider.enabled = false;
        player.controller.trailObject.SetActive(false);
    }

}
