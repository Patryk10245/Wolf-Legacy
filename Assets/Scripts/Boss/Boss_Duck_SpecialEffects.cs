using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Duck_SpecialEffects : MonoBehaviour
{
    public GameObject jumpSlam;
    public GameObject rushSpeed;
    public Transform slamInstantiatePosition;

    public void PlayJumpSlam()
    {
        Instantiate(jumpSlam, slamInstantiatePosition.position, transform.rotation);
    }
    public void PlayRushSpeed()
    {

    }
}
