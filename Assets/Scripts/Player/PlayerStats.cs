using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] float maxHealth = 5;
    [SerializeField] float currentHealth = 5;
    public float damageToEnemies = 1;
    public float currentEnergy = 40;
    float maxEnergy = 40;
    [SerializeField] float energyRegenerationAmount = 1;
    float energyRegenTimer;

    public void TakeDamage(float val)
    {
        currentHealth -= val;
        //Debug.Log("Taking Damage, Health = " + health);

        if(currentHealth <= 0)
        {
            Debug.LogWarning("Teraz powinna odbyc sie smierc gracza");
        }
    }
    public void ModifyEnergy(float val)
    {
        currentEnergy += val;
    }
    void EnergyRegeneration()
    {
        energyRegenTimer += Time.deltaTime;
        if(energyRegenTimer >= 1)
        {
            energyRegenTimer -= 1;
            ModifyEnergy(energyRegenerationAmount);
        }
    }

    private void Update()
    {
        
    }
}
