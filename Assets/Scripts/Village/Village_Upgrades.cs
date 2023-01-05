using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ENUM_UpgradeType
{
    damage,
    health,
    energy,
    energyRegen,
    speed
}

public class Village_Upgrades : MonoBehaviour
{
    public static Village_Upgrades ins;
    public void Reference()
    {
        ins = this;
    }
    private void Awake()
    {
        if(ins == null)
        {
            Reference();
            DontDestroyOnLoad(this);
        }
    }

    public ScoreTable scoreTable;


    public ClassUpgrades paladinUpgrades;
    public ClassUpgrades barbarianUpgrades;
    public ClassUpgrades rangerUpgrades;
    public ClassUpgrades mageUpgrades;

    public Village_UI_ClassInfo paladinInfo;
    public Village_UI_ClassInfo barbarianInfo;
    public Village_UI_ClassInfo rangerInfo;
    public Village_UI_ClassInfo mageInfo;

    bool Upgrade(ClassUpgrades characterclass, int type)
    {
        UpgradeLevel stat;
        switch (type)
        {
            case 0:
                stat = characterclass.damage;
                break;
            case 1:
                stat = characterclass.health;
                break;
            case 2:
                stat = characterclass.energy;
                break;
            case 3:
                stat = characterclass.energyRegeneration;
                break;
            case 4:
                stat = characterclass.speed;
                break;
            default:
                stat = characterclass.damage;
                break;

        }


        if (scoreTable.current_Gold > stat.cost[stat.currentLevel])
        {
            scoreTable.current_Gold -= stat.cost[stat.currentLevel];
            stat.currentLevel++;
            return true;
        }

        return false;

    }
    public void UpgradePaladin(int type)
    {
        if(Upgrade(paladinUpgrades, type) == true)
        {
            switch (type)
            {
                case 0:
                    paladinInfo.IncreaseDamageLevel(paladinUpgrades.damage.currentLevel);
                    break;
                case 1:
                    paladinInfo.IncreaseHealthLevel(paladinUpgrades.health.currentLevel);
                    break;
                case 2:
                    paladinInfo.IncreaseEnergyLevel(paladinUpgrades.energy.currentLevel);
                    break;
                case 3:
                    paladinInfo.IncreaseEnergyRegenLevel(paladinUpgrades.energyRegeneration.currentLevel);
                    break;
                case 4:
                    paladinInfo.IncreaseSpeedLevel(paladinUpgrades.speed.currentLevel);
                    break;
            }
        }
    }
    public void UpgradeBarbarian(int type)
    {
        
        if (Upgrade(barbarianUpgrades, type) == true)
        {
            switch (type)
            {
                case 0:
                    barbarianInfo.IncreaseDamageLevel(barbarianUpgrades.damage.currentLevel);
                    break;
                case 1:
                    barbarianInfo.IncreaseHealthLevel(barbarianUpgrades.health.currentLevel);
                    break;
                case 2:
                    barbarianInfo.IncreaseEnergyLevel(barbarianUpgrades.energy.currentLevel);
                    break;
                case 3:
                    barbarianInfo.IncreaseEnergyRegenLevel(barbarianUpgrades.energyRegeneration.currentLevel);
                    break;
                case 4:
                    barbarianInfo.IncreaseSpeedLevel(barbarianUpgrades.speed.currentLevel);
                    break;
            }
        }
    }
    public void UpgradeRanger(int type)
    {
        if (Upgrade(rangerUpgrades, type) == true)
        {
            switch (type)
            {
                case 0:
                    rangerInfo.IncreaseDamageLevel(rangerUpgrades.damage.currentLevel);
                    break;
                case 1:
                    rangerInfo.IncreaseHealthLevel(rangerUpgrades.health.currentLevel);
                    break;
                case 2:
                    rangerInfo.IncreaseEnergyLevel(rangerUpgrades.energy.currentLevel);
                    break;
                case 3:
                    rangerInfo.IncreaseEnergyRegenLevel(rangerUpgrades.energyRegeneration.currentLevel);
                    break;
                case 4:
                    rangerInfo.IncreaseSpeedLevel(rangerUpgrades.speed.currentLevel);
                    break;
            }
        }
    }
    public void UpgradeMage(int type)
    {
        if (Upgrade(mageUpgrades, type) == true)
        {
            switch (type)
            {
                case 0:
                    mageInfo.IncreaseDamageLevel(mageUpgrades.damage.currentLevel);
                    break;
                case 1:
                    mageInfo.IncreaseHealthLevel(mageUpgrades.health.currentLevel);
                    break;
                case 2:
                    mageInfo.IncreaseEnergyLevel(mageUpgrades.energy.currentLevel);
                    break;
                case 3:
                    mageInfo.IncreaseEnergyRegenLevel(mageUpgrades.energyRegeneration.currentLevel);
                    break;
                case 4:
                    mageInfo.IncreaseSpeedLevel(mageUpgrades.speed.currentLevel);
                    break;
            }
        }
    }


    public void LoadUpgrades()
    {

    }
    public void SaveUpgrades()
    {
        
    }



}
