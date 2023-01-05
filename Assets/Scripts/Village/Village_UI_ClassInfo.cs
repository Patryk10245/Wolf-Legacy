using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Village_UI_ClassInfo : MonoBehaviour
{
    public Image[] damageLevels;
    public Image[] healthLevels;
    public Image[] energyLevels;
    public Image[] energyRegenLevels;
    public Image[] speedLevels;

    public void IncreaseDamageLevel(int i)
    {
        damageLevels[i].color = Color.green;
    }
    public void IncreaseHealthLevel(int i)
    {
        healthLevels[i].color = Color.green;
    }
    public void IncreaseEnergyLevel(int i)
    {
        energyLevels[i].color = Color.green;
    }
    public void IncreaseEnergyRegenLevel(int i)
    {
        energyRegenLevels[i].color = Color.green;
    }
    public void IncreaseSpeedLevel(int i)
    {
        speedLevels[i].color = Color.green;
    }
}
