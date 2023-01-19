using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Optimization_AnimationWorker : MonoBehaviour
{
    public Animator anim;

    private void OnBecameInvisible()
    {
        anim.enabled = false;
    }

    private void OnBecameVisible()
    {
        anim.enabled = true;
    }
}
