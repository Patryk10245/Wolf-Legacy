using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_State : MonoBehaviour
{
    public static Game_State ins;

    public static bool gamePaused;
    public GameObject pausingWindow;
    public static bool levelLost;
    public static bool levelWon;


    private void Awake()
    {
        if (ins != null && ins != this)
        {
            Destroy(gameObject);
        }
        else
        {

            ins = this;
            DontDestroyOnLoad(this);
        }
    }
    void Update()
    {
    }
    public static void PauseGame()
    {
        gamePaused = !gamePaused;

        if (gamePaused)
        {
            Time.timeScale = 0;
            gamePaused = true;
            Level_PlayerUI_Control.ins.AnimationPauseGame();
        }
        else
        {
            Time.timeScale = 1;
            gamePaused = false;
            Level_PlayerUI_Control.ins.AnimationUnpauseGame();
        }
    }
    public void ResetValuesToDefault()
    {
        levelLost = false;
        levelWon = false;
        gamePaused = false;
        Time.timeScale = 1;
        Game_State.ins.pausingWindow.SetActive(false);
    }

    
}
