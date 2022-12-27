using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayerAttackStoper : MonoBehaviour
{
    public PlayerController playerController;

    public void animEndAttack()
    {
        playerController.inAttack = false;
        playerController.trail_renderer.enabled = false;
        playerController.hit_collider.player_hit_collider.enabled = false;
    }
}
