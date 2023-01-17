using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Stats : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float damage;

    [SerializeField] GameObject hitEffect;


    public void TakeDamage(float val)
    {
        AudioManager.ins.Play_EnemyHurt();
        currentHealth -= val;
        Instantiate(hitEffect, transform.position, transform.rotation);
    }
}
