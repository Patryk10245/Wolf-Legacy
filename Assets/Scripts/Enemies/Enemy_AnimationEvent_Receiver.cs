using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AnimationEvent_Receiver : MonoBehaviour
{
    public Enemy_BaseClass enemy;

    public void AnimEvent_MeleeAttack()
    {
        enemy.MeleeAttack_Action();
    }
    public void AnimEvent_RangedAttack()
    {

    }
    public void AnimEvent_Death()
    {
        enemy.Death();
    }
}
