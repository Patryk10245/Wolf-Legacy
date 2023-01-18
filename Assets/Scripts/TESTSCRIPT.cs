using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTSCRIPT : MonoBehaviour
{
    public Animator anim;

    private void OnBecameVisible()
    {
        //Debug.Log("I AM ALIVE = " + gameObject.name);
        anim.enabled = true;
    }
    private void OnBecameInvisible()
    {
        //Debug.Log("I AM DEAD = " + gameObject.name);
        anim.enabled = false;
    }

}
