using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // W razie potrzeby, statyczna referencja do gracza
    public static Player ins;
    public void Reference()
    {
        ins = this;
    }

    public PlayerController controller;
    public PlayerStats stats;
    

    private void Start()
    {
        Reference();
    }

    public void TakeDamage(float val)
    {
        stats.TakeDamage(val);
    }
}
