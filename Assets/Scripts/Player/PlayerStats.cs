using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] Player player;
    public float maxHealth;
    public float currentHealth;
    [Space(5)]
    public float maxEnergy;
    public float currentEnergy;
    public float energyRegenerationAmount = 1;

    public float healthRegenerationAmount = 0.25f;
    [Space(5)]
    public float damage;
    float timer;

    public void TakeDamage(float val)
    {
        currentHealth -= val;
        AudioManager.ins.Play_PlayerHurt();

        if(currentHealth <= 0)
        {
            //Debug.LogWarning("Teraz powinna odbyc sie smierc gracza");
            Level_FightReferenecs.ins.resurrection.PlayerDeath(player);
            player.isInvulnerable = true;
        }
    }
    public void HealPlayer(float val)
    {
        currentHealth += val;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        player.ui_updater.UpdateHealth();
    }
    public void ModifyEnergy(float val)
    {
        currentEnergy += val;
        player.ui_updater.UpdateEnergy();
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
            HealPlayer(healthRegenerationAmount);
        }
    }

    private void Update()
    {
        EnergyRegeneration();
    }
}
