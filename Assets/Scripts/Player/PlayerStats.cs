using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] float health_max;
    [SerializeField] float health;
    public float damage;

    public void TakeDamage(float val)
    {
        health -= val;
        //Debug.Log("Taking Damage, Health = " + health);

        if(health <= 0)
        {
            Debug.LogWarning("Teraz powinna odbyc sie smierc gracza");
        }
    }
}
