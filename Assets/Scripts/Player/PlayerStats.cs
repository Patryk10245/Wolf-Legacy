using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    [Space(5)]
    public float maxEnergy;
    public float currentEnergy;
    public float energyRegenerationAmount = 1;
    [Space(5)]
    public float damage;
    float timer;

    public void TakeDamage(float val)
    {
        currentHealth -= val;

        if(currentHealth <= 0)
        {
            Debug.LogWarning("Teraz powinna odbyc sie smierc gracza");
        }
    }
    public void ModifyEnergy(float val)
    {
        currentEnergy += val;
        if(currentEnergy > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }
    }
    void EnergyRegeneration()
    {
        timer += Time.deltaTime;
        if(timer >= 1)
        {
            timer -= 1;
            ModifyEnergy(energyRegenerationAmount);
        }
    }
    private void Update()
    {
        EnergyRegeneration();
    }
}
