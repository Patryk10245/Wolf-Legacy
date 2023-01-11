using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Ui_Updater : MonoBehaviour
{
    public Player player;
    public Image healthBar;

    public Image ability1Image;
    public Image ability2Image;

    bool updAbility1;
    bool updAbility2;

    public void UpdateHealth()
    {
        healthBar.fillAmount = player.stats.currentHealth / player.stats.maxHealth;
    }

    public void Ability1Used()
    {
        updAbility1 = true;
        ability1Image.fillAmount = 0;
    }
    public void Ability2Used()
    {
        updAbility2 = true;
        ability2Image.fillAmount = 0;
    }
    public void Ability1Recharged()
    {
        updAbility1 = false;
        ability1Image.fillAmount = 1;
    }
    public void Ability2Recharged()
    {
        updAbility2 = false;
        ability2Image.fillAmount = 1;
    }

    public void UpdateAbility1Timer()
    {
        ability1Image.fillAmount = player.abilityBasic.timer / player.abilityBasic.rechargeTime;
    }
    public void UpdateAbility2Timer()
    {
        ability2Image.fillAmount = player.abilitySecondary.timer / player.abilitySecondary.rechargeTime;
    }
    private void Update()
    {
        if(updAbility1)
        {
            UpdateAbility1Timer();
        }
        if(updAbility2)
        {
            UpdateAbility2Timer();
        }
    }

}
