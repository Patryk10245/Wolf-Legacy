using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Village_UI_Control : MonoBehaviour
{
    public static Village_UI_Control ins;
    public void Reference()
    {
        Debug.Log("Village ui Control INS reference");
        ins = this;
    }
    private void Awake()
    {
        
        Reference();
    }

    public Village_Upgrades villageUpgrades;
    public Village_UIMapChoosing mapChoosing;
    public Village_UI_UpgradeInfo upgradeInfo;
    [SerializeField] Animator canvasAnimator;
    [Header("Windows")]
    [SerializeField] GameObject infoField;
    [SerializeField] GameObject villageMenu;
    [SerializeField] Color opaqueColor;
    public Color boughtUpgradeColor;
    [Space(10)]
    [Header("Buildings")]
    [SerializeField] Image paladinDestroyedBuilding;
    [SerializeField] Image barbarianDestroyedBuilding;
    [SerializeField] Image archerDestroyedBuilding;
    [SerializeField] Image mageDestroyedBuilding;

    [SerializeField] GameObject paladinWindow;
    [SerializeField] GameObject barbarianWindow;
    [SerializeField] GameObject rangerWindow;
    [SerializeField] GameObject mageWindow;
    [Header("Buying Windows")]
    [SerializeField] GameObject paladinBuyingBuilding;
    [SerializeField] GameObject barbarianBuyingBuilding;
    [SerializeField] GameObject archerBuyingBuilding;
    [SerializeField] GameObject mageBuyingBuilding;

    [Header("Ui Info")]
    public Village_UI_ClassInfo paladinInfo;
    public Village_UI_ClassInfo barbarianInfo;
    public Village_UI_ClassInfo rangerInfo;
    public Village_UI_ClassInfo mageInfo;

    public GameObject[] paladinUpgradeMark;
    public GameObject[] barbarianUpgradeMark;
    public GameObject[] archerUpgradeMark;
    public GameObject[] mageUpgradeMark;

    public Text TextGoldAmount;


    public void SetReference()
    {
        //Debug.Log("Setting Reference for Village UI Control");
        //Debug.Log("upgrades = " + Village_Upgrades.ins.gameObject.name);
        villageUpgrades = GameSetup.ins.villageUpgrades;
        villageUpgrades.village_UI_Control = this;
        upgradeInfo.villageUpgrades = villageUpgrades;

        if(villageUpgrades.paladinBuildingBought == true)
        {
            ChangeBuildingToFixed(paladinBuyingBuilding, paladinDestroyedBuilding);
        }
        if (villageUpgrades.barbarianBuildingBought == true)
        {
            ChangeBuildingToFixed(barbarianBuyingBuilding, barbarianDestroyedBuilding);
        }
        if (villageUpgrades.archerBuildingBought == true)
        {
            ChangeBuildingToFixed(archerBuyingBuilding, archerDestroyedBuilding);
        }
        if (villageUpgrades.mageBuildingBought == true)
        {
            ChangeBuildingToFixed(mageBuyingBuilding, mageDestroyedBuilding);
        }

        if(Game_State.gameLost == true || Game_State.gameWon == true)
        {
            mapChoosing.DisableNextButton();
            mapChoosing.DisableRepeatButton();
        }

        ScoreTable.ins.TEXT_goldAmount = TextGoldAmount;

    }


    private void Update()
    {
    }

    void foo()
    {

    }

    public void ShowPaladinWindow()
    {
        Debug.Log("Activating Paladin Window");
        paladinWindow.SetActive(true);
        Debug.Log("Blocker state = " + paladinBuyingBuilding.activeSelf);
        if (villageUpgrades.paladinBuildingBought == false)
        {
            Debug.Log("If");
            paladinBuyingBuilding.SetActive(true);
            Debug.Log("Blocker state = " + paladinBuyingBuilding.activeSelf);
            return;
        }
        else
        {
            Debug.Log("Else");
            paladinBuyingBuilding.SetActive(false);
            Debug.Log("Blocker state = " + paladinBuyingBuilding.activeSelf);
        }
        Debug.Log("Running Animator");
        canvasAnimator.SetTrigger("openPaladin");
        //villageMenu.SetActive(false);
        
    }
    public void ShowBarbarianWindow()
    {
        barbarianWindow.SetActive(true);
        if (villageUpgrades.barbarianBuildingBought == false)
        {
            barbarianBuyingBuilding.SetActive(true);
            return;
        }
        else
        {
            barbarianBuyingBuilding.SetActive(false);
        }
        canvasAnimator.SetTrigger("openBarbarian");
        //villageMenu.SetActive(false);
        
    }
    public void ShowRangerWindow()
    {
        rangerWindow.SetActive(true);
        if (villageUpgrades.archerBuildingBought == false)
        {
            archerBuyingBuilding.SetActive(true);
            return;
        }
        else
        {
            archerBuyingBuilding.SetActive(false);
        }
        canvasAnimator.SetTrigger("openArcher");
        //villageMenu.SetActive(false);
        
    }
    public void ShowMageWindow()
    {
        mageWindow.SetActive(true);
        if (villageUpgrades.mageBuildingBought == false)
        {
            mageBuyingBuilding.SetActive(true);
            return;
        }
        else
        {
            mageBuyingBuilding.SetActive(false);
        }
        canvasAnimator.SetTrigger("openMage");
        //villageMenu.SetActive(false);
        
    }
    public void BuyBuilding(int building)
    {
        if(ScoreTable.ins.gold >= 150)
        {
            ScoreTable.ins.gold -= 150;
            switch (building)
            {
                case 0:
                    villageUpgrades.paladinBuildingBought = true;
                    canvasAnimator.SetTrigger("BuyPaladin");
                    break;
                case 1:
                    villageUpgrades.barbarianBuildingBought = true;
                    canvasAnimator.SetTrigger("BuyBarbarian");
                    break;
                case 2:
                    villageUpgrades.archerBuildingBought = true;
                    canvasAnimator.SetTrigger("BuyArcher");
                    break;
                case 3:
                    villageUpgrades.mageBuildingBought = true;
                    canvasAnimator.SetTrigger("BuyMage");
                    break;
            }
            
        }
    }

    public void FixPaladinBuilding()
    {
        ChangeBuildingToFixed(paladinBuyingBuilding, paladinDestroyedBuilding);
    }
    public void FixBarbarianBuilding()
    {
        ChangeBuildingToFixed(barbarianBuyingBuilding, barbarianDestroyedBuilding);
    }
    public void FixArcherBuilding()
    {
        ChangeBuildingToFixed(archerBuyingBuilding, archerDestroyedBuilding);
    }
    public void FixMageBuilding()
    {
        ChangeBuildingToFixed(mageBuyingBuilding, mageDestroyedBuilding);
    }
    public void ChangeBuildingToFixed(GameObject building, Image image)
    {
        building.SetActive(false);
        image.color = opaqueColor;
    }

    public void CloseUpgradeWindows()
    {
        Debug.Log("Closing WIndows");
        canvasAnimator.SetTrigger("closeWindows");
    }
    public void CloseBuyingWindows()
    {
        paladinBuyingBuilding.SetActive(false);
        barbarianBuyingBuilding.SetActive(false);
        archerBuyingBuilding.SetActive(false);
        mageBuyingBuilding.SetActive(false);
    }

    public void UpgradePaladin(int type)
    {
        //FixReference();
        villageUpgrades.UpgradePaladin(type);
        switch (type)
        {
            case 0:
                if(villageUpgrades.paladinUpgrades.damage.currentLevel >= 5)
                {
                    paladinUpgradeMark[type].SetActive(false);
                }
                break;
            case 1:
                if (villageUpgrades.paladinUpgrades.health.currentLevel >= 5)
                {
                    paladinUpgradeMark[type].SetActive(false);
                }
                break;
            case 2:
                if (villageUpgrades.paladinUpgrades.energy.currentLevel >= 5)
                {
                    paladinUpgradeMark[type].SetActive(false);
                }
                break;
            case 3:
                if (villageUpgrades.paladinUpgrades.energyRegeneration.currentLevel >= 5)
                {
                    paladinUpgradeMark[type].SetActive(false);
                }
                break;
            case 4:
                if (villageUpgrades.paladinUpgrades.speed.currentLevel >= 5)
                {
                    paladinUpgradeMark[type].SetActive(false);
                }
                break;
        }

        
    }
    public void UpgradeBarbarian(int type)
    {
        //FixReference();
        villageUpgrades.UpgradeBarbarian(type);;
        switch (type)
        {
            case 0:
                if (villageUpgrades.barbarianUpgrades.damage.currentLevel >= 5)
                {
                    barbarianUpgradeMark[type].SetActive(false);
                }
                break;
            case 1:
                if (villageUpgrades.barbarianUpgrades.health.currentLevel >= 5)
                {
                    barbarianUpgradeMark[type].SetActive(false);
                }
                break;
            case 2:
                if (villageUpgrades.barbarianUpgrades.energy.currentLevel >= 5)
                {
                    barbarianUpgradeMark[type].SetActive(false);
                }
                break;
            case 3:
                if (villageUpgrades.barbarianUpgrades.energyRegeneration.currentLevel >= 5)
                {
                    barbarianUpgradeMark[type].SetActive(false);
                }
                break;
            case 4:
                if (villageUpgrades.barbarianUpgrades.speed.currentLevel >= 5)
                {
                    barbarianUpgradeMark[type].SetActive(false);
                }
                break;
        }
    }
    public void UpgradeRanger(int type)
    {
        //FixReference();
        villageUpgrades.UpgradeRanger(type);

        switch (type)
        {
            case 0:
                if (villageUpgrades.archerUpgrades.damage.currentLevel >= 5)
                {
                    archerUpgradeMark[type].SetActive(false);
                }
                break;
            case 1:
                if (villageUpgrades.archerUpgrades.health.currentLevel >= 5)
                {
                    archerUpgradeMark[type].SetActive(false);
                }
                break;
            case 2:
                if (villageUpgrades.archerUpgrades.energy.currentLevel >= 5)
                {
                    archerUpgradeMark[type].SetActive(false);
                }
                break;
            case 3:
                if (villageUpgrades.archerUpgrades.energyRegeneration.currentLevel >= 5)
                {
                    archerUpgradeMark[type].SetActive(false);
                }
                break;
            case 4:
                if (villageUpgrades.archerUpgrades.speed.currentLevel >= 5)
                {
                    archerUpgradeMark[type].SetActive(false);
                }
                break;
        }
    }
    public void UpgradeMage(int type)
    {
        //FixReference();
        villageUpgrades.UpgradeMage(type);

        switch (type)
        {
            case 0:
                if (villageUpgrades.mageUpgrades.damage.currentLevel >= 5)
                {
                    mageUpgradeMark[type].SetActive(false);
                }
                break;
            case 1:
                if (villageUpgrades.mageUpgrades.health.currentLevel >= 5)
                {
                    mageUpgradeMark[type].SetActive(false);
                }
                break;
            case 2:
                if (villageUpgrades.mageUpgrades.energy.currentLevel >= 5)
                {
                    mageUpgradeMark[type].SetActive(false);
                }
                break;
            case 3:
                if (villageUpgrades.mageUpgrades.energyRegeneration.currentLevel >= 5)
                {
                    mageUpgradeMark[type].SetActive(false);
                }
                break;
            case 4:
                if (villageUpgrades.mageUpgrades.speed.currentLevel >= 5)
                {
                    mageUpgradeMark[type].SetActive(false);
                }
                break;
        }
    }

}
