using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AnimationEvent_Receiver : MonoBehaviour
{
    public Player player;
    public void animEndAttack()
    {
        player.inAttack = false;
        player.controller.weaponCollider.enabled = false;
        player.controller.trailObject.SetActive(false);
    }

}
