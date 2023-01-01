using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Ui_Updater : MonoBehaviour
{
    public Player player;
    public Image healthBar;
    public Text goldAmount;

    public void UpdateHealth()
    {
        healthBar.fillAmount = player.stats.currentHealth / player.stats.maxHealth;
    }
    
}
