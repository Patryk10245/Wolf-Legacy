using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayerAttackStoper : MonoBehaviour
{
    public PlayerController playerController;

    public void animEndAttack()
    {
        playerController.inAttack = false;
    }
}
