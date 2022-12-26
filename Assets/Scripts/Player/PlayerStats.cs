using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] float health_max;
    [SerializeField] float health;
    public float damage;
    public float energy = 40;
    [SerializeField] float max_energy = 40;
    [SerializeField] float energy_regeneration = 1;
    float timer;

    public void TakeDamage(float val)
    {
        health -= val;
        //Debug.Log("Taking Damage, Health = " + health);

        if(health <= 0)
        {
            Debug.LogWarning("Teraz powinna odbyc sie smierc gracza");
        }
    }
    private void Update()
    {
        Energy_Regen();
    }

    private void Energy_Regen()
    {
        timer += Time.deltaTime;
        if (timer >= 0)
        {
            timer -= 1;
            energy += energy_regeneration;

            if(energy > max_energy)
            {
                energy = max_energy;
            }
        }
    }
    public void ModifyEnergy(float val)
    {
        energy += val;
    }
}
