using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerStats stats;
    public Camera theCam;
    

    public int id;

    public bool prohibitAllActions;
    public bool canMove;
    public bool inAttack;
    public bool canRotateSword;


    private void Start()
    {
        //controller = GetComponent<PlayerController>();
        stats = GetComponent<PlayerStats>();
    }

    public void TakeDamage(float val)
    {
        stats.TakeDamage(val);
    }

    public void KnockBack(Vector3 force)
    {
        Debug.Log("Knockback");
        //controller.rb.AddForce(force);
        controller.rb.AddForce(force);
    }
    void CameraFollow()
    {
        theCam.transform.position = new Vector3(transform.position.x, transform.position.y, -8f);
    }
    private void Update()
    {
        CameraFollow();
    }
}
