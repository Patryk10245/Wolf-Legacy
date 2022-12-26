using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Stats : MonoBehaviour
{
    Enemy_BaseClass enemy;
    [SerializeField] float health_Max;
    [SerializeField] float health;
    [SerializeField] float damage;

    public float Damage { get => damage; set => damage = value; }

    public void TakeDamage(float val)
    {
        //Debug.Log("Take Damage");
        health -= val;
        if(health <= 0)
        {
            enemy.Death();
            //Debug.LogWarning("Przeciwnik umiera");
        }
    }
    private void Start()
    {
        enemy = GetComponent<Enemy_BaseClass>();
    }

}
