using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTSCRIPT : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector3 force;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("k"))
        {
            rb.AddForce(force);
        }
    }
}
