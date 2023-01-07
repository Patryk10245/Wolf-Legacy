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
        Debug.Log("Setting reference for Village Upgraes");
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
    private void Start()
    {
        Debug.Log("Setting village control refernec from start");
        village_UI_Control = Village_UI_Control.ins;
    }

    public ScoreTable scoreTable;


    public ClassUpgrades paladinUpgrades;
    public ClassUpgrades barbarianUpgrades;
    public ClassUpgrades rangerUpgrades;
    public ClassUpgrades mageUpgrades;

    public Village_UI_Control village_UI_Control;

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

        Debug.Log(scoreTable.gameObject);
        Debug.Log(stat.currentLevel);

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
        if (Upgrade(paladinUpgrades, type) == true)
        {
            switch (type)
            {
                case 0:
                    village_UI_Control.paladinInfo.IncreaseDamageLevel(paladinUpgrades.damage.currentLevel);
                    break;
                case 1:
                    village_UI_Control.paladinInfo.IncreaseHealthLevel(paladinUpgrades.health.currentLevel);
                    break;
                case 2:
                    village_UI_Control.paladinInfo.IncreaseEnergyLevel(paladinUpgrades.energy.currentLevel);
                    break;
                case 3:
                    village_UI_Control.paladinInfo.IncreaseEnergyRegenLevel(paladinUpgrades.energyRegeneration.currentLevel);
                    break;
                case 4:
                    village_UI_Control.paladinInfo.IncreaseSpeedLevel(paladinUpgrades.speed.currentLevel);
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
                    village_UI_Control.barbarianInfo.IncreaseDamageLevel(barbarianUpgrades.damage.currentLevel);
                    break;
                case 1:
                    village_UI_Control.barbarianInfo.IncreaseHealthLevel(barbarianUpgrades.health.currentLevel);
                    break;
                case 2:
                    village_UI_Control.barbarianInfo.IncreaseEnergyLevel(barbarianUpgrades.energy.currentLevel);
                    break;
                case 3:
                    village_UI_Control.barbarianInfo.IncreaseEnergyRegenLevel(barbarianUpgrades.energyRegeneration.currentLevel);
                    break;
                case 4:
                    village_UI_Control.barbarianInfo.IncreaseSpeedLevel(barbarianUpgrades.speed.currentLevel);
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
                    village_UI_Control.rangerInfo.IncreaseDamageLevel(rangerUpgrades.damage.currentLevel);
                    break;
                case 1:
                    village_UI_Control.rangerInfo.IncreaseHealthLevel(rangerUpgrades.health.currentLevel);
                    break;
                case 2:
                    village_UI_Control.rangerInfo.IncreaseEnergyLevel(rangerUpgrades.energy.currentLevel);
                    break;
                case 3:
                    village_UI_Control.rangerInfo.IncreaseEnergyRegenLevel(rangerUpgrades.energyRegeneration.currentLevel);
                    break;
                case 4:
                    village_UI_Control.rangerInfo.IncreaseSpeedLevel(rangerUpgrades.speed.currentLevel);
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
                    village_UI_Control.mageInfo.IncreaseDamageLevel(mageUpgrades.damage.currentLevel);
                    break;
                case 1:
                    village_UI_Control.mageInfo.IncreaseHealthLevel(mageUpgrades.health.currentLevel);
                    break;
                case 2:
                    village_UI_Control.mageInfo.IncreaseEnergyLevel(mageUpgrades.energy.currentLevel);
                    break;
                case 3:
                    village_UI_Control.mageInfo.IncreaseEnergyRegenLevel(mageUpgrades.energyRegeneration.currentLevel);
                    break;
                case 4:
                    village_UI_Control.mageInfo.IncreaseSpeedLevel(mageUpgrades.speed.currentLevel);
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
