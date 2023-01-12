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
    [Header("Windows")]
    public GameObject infoField;
    public GameObject villageMenu;
    public Color opaqueColor;
    [Space(10)]
    [Header("Buildngs")]
    public Image paladinBuilding;
    public Image barbarianBuilding;
    public Image archerBuilding;
    public Image mageBuilding;

    public GameObject paladinWindow;
    public GameObject barbarianWindow;
    public GameObject rangerWindow;
    public GameObject mageWindow;
    [Header("Buying Windows")]
    public GameObject paladinBuyingBuilding;
    public GameObject barbarianBuyingBuilding;
    public GameObject archerBuyingBuilding;
    public GameObject mageBuyingBuilding;

    public Sprite palBrokenBuilding;
    public Sprite barBrokenBuilding;
    public Sprite arcBrokenBuilding;
    public Sprite magBrokenBuilding;

    [Header("Ui Info")]
    public Village_UI_ClassInfo paladinInfo;
    public Village_UI_ClassInfo barbarianInfo;
    public Village_UI_ClassInfo rangerInfo;
    public Village_UI_ClassInfo mageInfo;


    public void SetReference()
    {
        Debug.Log("Setting Reference for Village UI Control");
        //Debug.Log("upgrades = " + Village_Upgrades.ins.gameObject.name);
        villageUpgrades = GameSetup.ins.villageUpgrades;
        villageUpgrades.village_UI_Control = this;

        if(villageUpgrades.paladinBuildingBought == false)
        {
            paladinBuilding.color = opaqueColor;
        }
        if (villageUpgrades.barbarianBuildingBought == false)
        {
            barbarianBuilding.color = opaqueColor;
        }
        if (villageUpgrades.archerBuildingBought == false)
        {
            archerBuilding.color = opaqueColor;
        }
        if (villageUpgrades.mageBuildingBought == false)
        {
            mageBuilding.color = opaqueColor;
        }

    }


    private void Update()
    {
        foo();
    }

    void foo()
    {
        if(Input.GetKeyDown("l"))
        {
            Debug.Log("CHANGE BUILDING");
            paladinBuilding.color = opaqueColor;
        }
    }

    public void ShowPaladinWindow()
    {
        paladinWindow.SetActive(true);
        villageMenu.SetActive(false);
        if(villageUpgrades.paladinBuildingBought == true)
        {
            paladinBuyingBuilding.SetActive(false);
        }
    }
    public void ShowBarbarianWindow()
    {
        villageMenu.SetActive(false);
        barbarianWindow.SetActive(true);
        if (villageUpgrades.barbarianBuildingBought == true)
        {
            barbarianBuyingBuilding.SetActive(false);
        }
    }
    public void ShowRangerWindow()
    {
        villageMenu.SetActive(false);
        rangerWindow.SetActive(true);
        if (villageUpgrades.archerBuildingBought == true)
        {
            archerBuyingBuilding.SetActive(false);
        }
    }
    public void ShowMageWindow()
    {
        villageMenu.SetActive(false);
        mageWindow.SetActive(true);
        if (villageUpgrades.mageBuildingBought == true)
        {
            mageBuyingBuilding.SetActive(false);
        }
    }
    public void BuyBuilding(int building)
    {
        if(ScoreTable.ins.gold >= 150)
        {
            switch (building)
            {
                case 0:
                    villageUpgrades.paladinBuildingBought = true;
                    paladinBuyingBuilding.SetActive(false);
                    paladinBuilding.color = opaqueColor;
                    break;
                case 1:
                    villageUpgrades.barbarianBuildingBought = true;
                    barbarianBuyingBuilding.SetActive(false);
                    barbarianBuilding.color = opaqueColor;
                    break;
                case 2:
                    villageUpgrades.archerBuildingBought = true;
                    archerBuyingBuilding.SetActive(false);
                    archerBuilding.color = opaqueColor;
                    break;
                case 3:
                    villageUpgrades.mageBuildingBought = true;
                    mageBuyingBuilding.SetActive(false);
                    mageBuilding.color = opaqueColor;
                    break;
            }
            
        }
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

    public void NextMap()
    {
        if(GameSetup.ins.playingPlayers[0].isDead)
        {
            GameSetup.ins.SaveClassData();
            Level_SelectedScenes.ins.ChangeToMainmenu();
            return;
        }
        GameSetup.ins.SaveClassData();


        switch(GameSetup.ins.lastFightMap)
        {
            case 1:
                Level_SelectedScenes.ins.ChangeToMap2();
                break;
            case 2:
                Level_SelectedScenes.ins.ChangeToMap3();
                break;
            case 3:
                break;
        }
        
    }
}
