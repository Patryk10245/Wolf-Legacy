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
    [SerializeField] Animator canvasAnimator;
    [Header("Windows")]
    [SerializeField] GameObject infoField;
    [SerializeField] GameObject villageMenu;
    [SerializeField] Color opaqueColor;
    [Space(10)]
    [Header("Buildngs")]
    [SerializeField] Image paladinBuilding;
    [SerializeField] Image barbarianBuilding;
    [SerializeField] Image archerBuilding;
    [SerializeField] Image mageBuilding;

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

    public Text TextGoldAmount;


    public void SetReference()
    {
        //Debug.Log("Setting Reference for Village UI Control");
        //Debug.Log("upgrades = " + Village_Upgrades.ins.gameObject.name);
        villageUpgrades = GameSetup.ins.villageUpgrades;
        villageUpgrades.village_UI_Control = this;

        if(villageUpgrades.paladinBuildingBought == true)
        {
            paladinBuilding.color = opaqueColor;
        }
        if (villageUpgrades.barbarianBuildingBought == true)
        {
            barbarianBuilding.color = opaqueColor;
        }
        if (villageUpgrades.archerBuildingBought == true)
        {
            archerBuilding.color = opaqueColor;
        }
        if (villageUpgrades.mageBuildingBought == true)
        {
            mageBuilding.color = opaqueColor;
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
        paladinWindow.SetActive(true);
        //villageMenu.SetActive(false);
        if(villageUpgrades.paladinBuildingBought == true)
        {
            paladinBuyingBuilding.SetActive(false);
        }
    }
    public void ShowBarbarianWindow()
    {
        barbarianWindow.SetActive(true);
        //villageMenu.SetActive(false);
        if (villageUpgrades.barbarianBuildingBought == true)
        {
            barbarianBuyingBuilding.SetActive(false);
        }
    }
    public void ShowRangerWindow()
    {
        rangerWindow.SetActive(true);
        //villageMenu.SetActive(false);
        if (villageUpgrades.archerBuildingBought == true)
        {
            archerBuyingBuilding.SetActive(false);
        }
    }
    public void ShowMageWindow()
    {
        mageWindow.SetActive(true);
        //villageMenu.SetActive(false);
        if (villageUpgrades.mageBuildingBought == true)
        {
            mageBuyingBuilding.SetActive(false);
        }
    }
    public void BuyBuilding(int building)
    {
        if(ScoreTable.ins.gold >= 150)
        {
            ScoreTable.ins.AddGold(-150);
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
        ChangeBuildingToFixed(paladinBuyingBuilding, paladinBuilding);
    }
    public void FixBarbarianBuilding()
    {
        ChangeBuildingToFixed(barbarianBuyingBuilding, barbarianBuilding);
    }
    public void FixArcherBuilding()
    {
        ChangeBuildingToFixed(archerBuyingBuilding, archerBuilding);
    }
    public void FixMageBuilding()
    {
        ChangeBuildingToFixed(mageBuyingBuilding, mageBuilding);
    }
    public void ChangeBuildingToFixed(GameObject building, Image image)
    {
        building.SetActive(false);
        image.color = opaqueColor;
    }

    public void CloseWindows()
    {
        paladinWindow.SetActive(false);
        barbarianWindow.SetActive(false);
        rangerWindow.SetActive(false);
        mageWindow.SetActive(false);
        villageMenu.SetActive(true);
    }


    public void SetPos()
    {

    }

    void FixReference()
    {
        if (villageUpgrades == null)
        {
            villageUpgrades = Village_Upgrades.ins;
            villageUpgrades.village_UI_Control = this; ;
        }
    }

    public void UpgradePaladin(int type)
    {
        //FixReference();
        villageUpgrades.UpgradePaladin(type);
    }
    public void UpgradeBarbarian(int type)
    {
        //FixReference();
        villageUpgrades.UpgradeBarbarian(type);
    }
    public void UpgradeRanger(int type)
    {
        //FixReference();
        villageUpgrades.UpgradeRanger(type);
    }
    public void UpgradeMage(int type)
    {
        //FixReference();
        villageUpgrades.UpgradeMage(type);
    }

}
