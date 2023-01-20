using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Village_UI_UpgradeInfo : MonoBehaviour
{
    public GameObject originalParent;
    public GameObject infoField;
    public Text textField;
    public Village_Upgrades villageUpgrades;

    public void PointerExit()
    {
        infoField.transform.SetParent(originalParent.transform);
        infoField.transform.localPosition = Vector3.zero;
    }

    public void PointerEnterPaladin(int type)
    {
        infoField.transform.SetParent(Village_UI_Control.ins.paladinUpgradeMark[type].transform);
        infoField.transform.localPosition = new Vector3(120, 0, 0);

        switch (type)
        {
            case 0:
                textField.text = villageUpgrades.paladinUpgrades.damage.cost[villageUpgrades.paladinUpgrades.damage.currentLevel].ToString();
                break;
            case 1:
                textField.text = villageUpgrades.paladinUpgrades.health.cost[villageUpgrades.paladinUpgrades.health.currentLevel].ToString();
                break;
            case 2:
                textField.text = villageUpgrades.paladinUpgrades.energy.cost[villageUpgrades.paladinUpgrades.energy.currentLevel].ToString();
                break;
            case 3:
                textField.text = villageUpgrades.paladinUpgrades.energyRegeneration.cost[villageUpgrades.paladinUpgrades.energyRegeneration.currentLevel].ToString();
                break;
            case 4:
                textField.text = villageUpgrades.paladinUpgrades.speed.cost[villageUpgrades.paladinUpgrades.speed.currentLevel].ToString();
                break;
        }
    }

    public void PointerEnterBarbarian(int type)
    {
        infoField.transform.SetParent(Village_UI_Control.ins.barbarianUpgradeMark[type].transform);
        infoField.transform.localPosition = new Vector3(120, 0, 0);

        switch (type)
        {
            case 0:
                textField.text = villageUpgrades.barbarianUpgrades.damage.cost[villageUpgrades.barbarianUpgrades.damage.currentLevel].ToString();
                break;
            case 1:
                textField.text = villageUpgrades.barbarianUpgrades.health.cost[villageUpgrades.barbarianUpgrades.health.currentLevel].ToString();
                break;
            case 2:
                textField.text = villageUpgrades.barbarianUpgrades.energy.cost[villageUpgrades.barbarianUpgrades.energy.currentLevel].ToString();
                break;
            case 3:
                textField.text = villageUpgrades.barbarianUpgrades.energyRegeneration.cost[villageUpgrades.barbarianUpgrades.energyRegeneration.currentLevel].ToString();
                break;
            case 4:
                textField.text = villageUpgrades.barbarianUpgrades.speed.cost[villageUpgrades.barbarianUpgrades.speed.currentLevel].ToString();
                break;
        }
    }

    public void PointerEnterArcher(int type)
    {
        infoField.transform.SetParent(Village_UI_Control.ins.archerUpgradeMark[type].transform);
        infoField.transform.localPosition = new Vector3(120, 0, 0);

        switch (type)
        {
            case 0:
                textField.text = villageUpgrades.archerUpgrades.damage.cost[villageUpgrades.archerUpgrades.damage.currentLevel].ToString();
                break;
            case 1:
                textField.text = villageUpgrades.archerUpgrades.health.cost[villageUpgrades.archerUpgrades.health.currentLevel].ToString();
                break;
            case 2:
                textField.text = villageUpgrades.archerUpgrades.energy.cost[villageUpgrades.archerUpgrades.energy.currentLevel].ToString();
                break;
            case 3:
                textField.text = villageUpgrades.archerUpgrades.energyRegeneration.cost[villageUpgrades.archerUpgrades.energyRegeneration.currentLevel].ToString();
                break;
            case 4:
                textField.text = villageUpgrades.archerUpgrades.speed.cost[villageUpgrades.archerUpgrades.speed.currentLevel].ToString();
                break;
        }
    }
    public void PointerEnterMage(int type)
    {
        infoField.transform.SetParent(Village_UI_Control.ins.mageUpgradeMark[type].transform);
        infoField.transform.localPosition = new Vector3(120, 0, 0);

        switch (type)
        {
            case 0:
                textField.text = villageUpgrades.mageUpgrades.damage.cost[villageUpgrades.mageUpgrades.damage.currentLevel].ToString();
                break;
            case 1:
                textField.text = villageUpgrades.mageUpgrades.health.cost[villageUpgrades.mageUpgrades.health.currentLevel].ToString();
                break;
            case 2:
                textField.text = villageUpgrades.mageUpgrades.energy.cost[villageUpgrades.mageUpgrades.energy.currentLevel].ToString();
                break;
            case 3:
                textField.text = villageUpgrades.mageUpgrades.energyRegeneration.cost[villageUpgrades.mageUpgrades.energyRegeneration.currentLevel].ToString();
                break;
            case 4:
                textField.text = villageUpgrades.mageUpgrades.speed.cost[villageUpgrades.mageUpgrades.speed.currentLevel].ToString();
                break;
        }
    }
}
