using UnityEngine;
using UnityEngine.UI;

public class Village_UI_ClassInfo : MonoBehaviour
{
    public Image[] damageLevels;
    public Image[] healthLevels;
    public Image[] energyLevels;
    public Image[] energyRegenLevels;
    public Image[] speedLevels;


    public void UI_IncreaseDamageLevel(int x)
    {
        for (int i = 0; i < x; i++)
        {
            damageLevels[i].color = Village_UI_Control.ins.boughtUpgradeColor;
        }
    }
    public void UI_IncreaseHealthLevel(int x)
    {
        for (int i = 0; i < x; i++)
        {
            healthLevels[i].color = Village_UI_Control.ins.boughtUpgradeColor;
        }
    }
    public void UI_IncreaseEnergyLevel(int x)
    {
        for (int i = 0; i < x; i++)
        {
            energyLevels[i].color = Village_UI_Control.ins.boughtUpgradeColor;
        }
    }
    public void UI_IncreaseEnergyRegenLevel(int x)
    {
        for (int i = 0; i < x; i++)
        {
            energyRegenLevels[i].color = Village_UI_Control.ins.boughtUpgradeColor;

        }
    }
    public void UI_IncreaseSpeedLevel(int x)
    {
        for (int i = 0; i < x; i++)
        {
            speedLevels[i].color = Village_UI_Control.ins.boughtUpgradeColor;
        }
    }
}
