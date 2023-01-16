using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_GameCompleted : MonoBehaviour
{
    public static Level_GameCompleted ins;

    public GameObject finishScreen;
    public Text collectedGoldText; 
    private void Awake()
    {
        ins = this;
    }
    public void ShowLevelCompletedScreen()
    {
        if(Level_FightReferenecs.ins.isLastMap == true)
        {
            Game_State.gameWon = true;   
        }
        finishScreen.gameObject.SetActive(true);
    }

    public void UI_ReturnToVillage()
    {
        Level_FightReferenecs.ins.playerManager.DestroyAllPlayers();
        GameSetup.ins.SaveClassData();
        Level_SelectedScenes.ins.ChangeToVillageScene();
    }
    public void UI_ReturnToMenu()
    {
        Level_FightReferenecs.ins.playerManager.DestroyAllPlayers();
        GameSetup.ins.SaveClassData();
        Level_SelectedScenes.ins.ChangeToMainmenu();
    }
}
