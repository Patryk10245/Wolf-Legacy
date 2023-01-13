using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENUM_AttackType
{
    melee,
    ranged
}


[RequireComponent(typeof(PlayerStats))]
public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerStats stats;
    public Camera currentCamera;
    public Player_Ui_Updater ui_updater;

    public Player_AttackScript attackScript;
    public Ability_1 abilityBasic;
    public Ability_2 abilitySecondary;
    public ENUM_PlayerClass playerClass;
    

    public int id;

    public bool prohibitAllActions;
    public bool canMove;
    public bool inAttack;
    public bool canRotateWeapon;
    public bool isInvulnerable;
    public bool isDead;

    [Header("Debug")]
    public GameObject dotobject;
    public GameObject shield;
    public float dot;
    public Vector2 dotdir;
    public Vector2 playerdir;

    private void Start()
    {
        //Debug.Log("start for Player = " + gameObject.name);
        //controller = GetComponent<PlayerController>();
        stats = GetComponent<PlayerStats>();
        controller = GetComponent<PlayerController>();
        ui_updater = GetComponent<Player_Ui_Updater>();
        attackScript = GetComponent<Player_AttackScript>();
        abilityBasic = GetComponent<Ability_1>();
        currentCamera = Camera.main;
    }

    public void TakeDamage(float val)
    {
        if(isInvulnerable == true)
        {
            return;
        }

        stats.TakeDamage(val);
        ui_updater.UpdateHealth();
    }
    public void TakeDamage(float val, Vector3 dir)
    {
        if(playerClass == ENUM_PlayerClass.Paladin)
        {
            Vector3 playersDirectionV3 = shield.transform.position - gameObject.transform.position;
            Vector2 playersDirection = playersDirectionV3;
            Vector2 projectilesDirection = dir;

            dot = Vector2.Dot(projectilesDirection, playersDirection);
            if(dot < -0.7f)
            {
                TakeDamage(val);
            }
        }
    }

    public void KnockBack(Vector3 force)
    {
        //Debug.Log("Knockback");
        //controller.rb.AddForce(force);
        controller.rb.AddForce(force);
    }
    private void Update()
    {
        if(dotobject != null)
        {
            //dot = Vector3.Dot(gameObject.transform.forward, dir);
            Vector2 projectilesPosition = new Vector2(dotobject.transform.position.x, dotobject.transform.position.y);
            Vector2 playersPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            dotdir = (playersPosition - projectilesPosition).normalized;
            Vector2 shieldsPosition = new Vector2(shield.transform.position.x, shield.transform.position.y);
            playerdir = shieldsPosition - playersPosition;
            playerdir.Normalize();
            dot = Vector2.Dot(dotdir, playerdir);

            
        }

        if (Input.GetKeyDown("k"))
        {
            dotobject.GetComponent<Rigidbody2D>().AddForce(dotdir * 50);
        }

        
    }
    private void OnDrawGizmos()
    {
        //Gizmos.DrawRay(dotobject.transform.position, dotdir);
        //Gizmos.DrawRay(gameObject.transform.position, playerdir);
    }
}
