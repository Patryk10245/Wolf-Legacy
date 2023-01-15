using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_GameOver : MonoBehaviour
{
    public GameObject deathScreen;

    public void InitiateGameOver()
    {
       
        foreach(PlayerSelectedData player in GameSetup.ins.playingPlayers)
        {
            player.isDead = true;
        }
        Game_State.gamePaused = true;
        Time.timeScale = 0;

        ScoreTable.ins.ReduceCollectedGoldByDeath();
        ScoreTable.ins.ApplyCollectedGold();
        deathScreen.SetActive(true);
        GameSetup.ins.SaveClassData();
    }

    public void UI_ReturnToVillage()
    {
        Debug.Log("Game lost = " + Game_State.gameLost);
        Level_SelectedScenes.ins.ChangeToVillageScene();
    }
    public void UI_ReturnToMenu()
    {
        Level_SelectedScenes.ins.ChangeToMainmenu();
    }
}
