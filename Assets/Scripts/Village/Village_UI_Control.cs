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
    public GameObject infoField;

    public GameObject paladinWindow;
    public GameObject barbarianWindow;
    public GameObject rangerWindow;
    public GameObject mageWindow;

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
        
    }

    


    public void ShowPaladinWindow()
    {
        paladinWindow.SetActive(true);
    }
    public void ShowBarbarianWindow()
    {
        barbarianWindow.SetActive(true);
    }
    public void ShowRangerWindow()
    {
        rangerWindow.SetActive(true);
    }
    public void ShowMageWindow()
    {
        mageWindow.SetActive(true);
    }

    public void CloseWindows()
    {
        paladinWindow.SetActive(false);
        barbarianWindow.SetActive(false);
        rangerWindow.SetActive(false);
        mageWindow.SetActive(false);
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
        FixReference();
        villageUpgrades.UpgradePaladin(type);
    }
    public void UpgradeBarbarian(int type)
    {
        FixReference();
        villageUpgrades.UpgradeBarbarian(type);
    }
    public void UpgradeRanger(int type)
    {
        FixReference();
        villageUpgrades.UpgradeRanger(type);
    }
    public void UpgradeMage(int type)
    {
        FixReference();
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
