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

    void Upgrade(ClassUpgrades characterclass, int type)
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
        }

    }
    public void UpgradePaladin(int type)
    {
        Upgrade(paladinUpgrades, type);
    }
    public void UpgradeBarbarian(int type)
    {
        Upgrade(barbarianUpgrades, type);
    }
    public void UpgradeRanger(int type)
    {
        Upgrade(rangerUpgrades, type);
    }
    public void UpgradeMage(int type)
    {
        Upgrade(mageUpgrades, type);
    }




}
