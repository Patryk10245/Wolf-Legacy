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
        //Debug.Log("Setting reference for Village Upgraes");
        ins = this;
    }
    private void Awake()
    {
        if (ins == null)
        {
            Reference();
            DontDestroyOnLoad(this);
        }
    }
    private void Start()
    {
        //Debug.Log("Setting village control refernec from start");
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

        //Debug.Log(scoreTable.gameObject);
        //Debug.Log(stat.currentLevel);

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
            UpdatePaladinUI();
        }
    }
    public void UpgradeBarbarian(int type)
    {
        if (Upgrade(barbarianUpgrades, type) == true)
        {
            UpdateBarbarianUI();
        }
    }
    public void UpgradeRanger(int type)
    {
        if (Upgrade(rangerUpgrades, type) == true)
        {
            UpdateRangernUI();
        }
    }
    public void UpgradeMage(int type)
    {
        if (Upgrade(mageUpgrades, type) == true)
        {
            UpdateMageUI();
        }
    }

    void UpdatePaladinUI()
    {
        village_UI_Control.paladinInfo.UI_IncreaseDamageLevel(paladinUpgrades.damage.currentLevel);
        village_UI_Control.paladinInfo.UI_IncreaseHealthLevel(paladinUpgrades.health.currentLevel);
        village_UI_Control.paladinInfo.UI_IncreaseEnergyLevel(paladinUpgrades.energy.currentLevel);
        village_UI_Control.paladinInfo.UI_IncreaseEnergyRegenLevel(paladinUpgrades.energyRegeneration.currentLevel);
        village_UI_Control.paladinInfo.UI_IncreaseSpeedLevel(paladinUpgrades.speed.currentLevel);
    }
    void UpdateBarbarianUI()
    {
        village_UI_Control.barbarianInfo.UI_IncreaseDamageLevel(barbarianUpgrades.damage.currentLevel);
        village_UI_Control.barbarianInfo.UI_IncreaseHealthLevel(barbarianUpgrades.health.currentLevel);
        village_UI_Control.barbarianInfo.UI_IncreaseEnergyLevel(barbarianUpgrades.energy.currentLevel);
        village_UI_Control.barbarianInfo.UI_IncreaseEnergyRegenLevel(barbarianUpgrades.energyRegeneration.currentLevel);
        village_UI_Control.barbarianInfo.UI_IncreaseSpeedLevel(barbarianUpgrades.speed.currentLevel);
    }
    void UpdateRangernUI()
    {
        village_UI_Control.rangerInfo.UI_IncreaseDamageLevel(rangerUpgrades.damage.currentLevel);
        village_UI_Control.rangerInfo.UI_IncreaseHealthLevel(rangerUpgrades.health.currentLevel);
        village_UI_Control.rangerInfo.UI_IncreaseEnergyLevel(rangerUpgrades.energy.currentLevel);
        village_UI_Control.rangerInfo.UI_IncreaseEnergyRegenLevel(rangerUpgrades.energyRegeneration.currentLevel);
        village_UI_Control.rangerInfo.UI_IncreaseSpeedLevel(rangerUpgrades.speed.currentLevel);
    }
    void UpdateMageUI()
    {
        village_UI_Control.mageInfo.UI_IncreaseDamageLevel(mageUpgrades.damage.currentLevel);
        village_UI_Control.mageInfo.UI_IncreaseHealthLevel(mageUpgrades.health.currentLevel);
        village_UI_Control.mageInfo.UI_IncreaseEnergyLevel(mageUpgrades.energy.currentLevel);
        village_UI_Control.mageInfo.UI_IncreaseEnergyRegenLevel(mageUpgrades.energyRegeneration.currentLevel);
        village_UI_Control.mageInfo.UI_IncreaseSpeedLevel(mageUpgrades.speed.currentLevel);
    }

    public void UpdateClassesUIUpgrades()
    {
        UpdatePaladinUI();
        UpdateBarbarianUI();
        UpdateRangernUI();
        UpdateMageUI();
    }

    public void LoadUpgrades()
    {

    }
    public void SaveUpgrades()
    {

    }



}
