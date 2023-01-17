using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_GameOver : MonoBehaviour
{
    public GameObject deathScreen;

    public void InitiateGameOver()
    {
        Game_State.gamePaused = true;
        Time.timeScale = 0;
        Game_CanvasController.ins.DeathScreen();
        ScoreTable.ins.ReduceCollectedGoldByDeath();
        //ScoreTable.ins.ApplyCollectedGold();
        GameSetup.ins.SaveClassData();

    }

    public void UI_ReturnToVillage()
    {
        ScoreTable.ins.ApplyCollectedGold();
        Level_SelectedScenes.ins.ChangeToVillageScene();
    }
    public void UI_ReturnToMenu()
    {
        ScoreTable.ins.ApplyCollectedGold();
        Level_SelectedScenes.ins.ChangeToMainmenu();
    }
}
