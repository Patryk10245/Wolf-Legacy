using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_State : MonoBehaviour
{
    public static Game_State ins;

    public static bool gamePaused;
    public GameObject pausingWindow;
    public static bool gameLost;
    public static bool gameWon;


    private void Awake()
    {
        if (ins != null && ins != this)
        {
            //Debug.Log("Destroying = " + gameObject);
            //Debug.Log("scene = " + gameObject.scene.name);
            Destroy(gameObject);
        }
        else
        {

            ins = this;
            //Debug.Log("scene = " + gameObject.scene.name);
            DontDestroyOnLoad(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    public static void PauseGame()
    {
        gamePaused = !gamePaused;

        if (gamePaused)
        {
            Debug.Log("Pausing Game, Time Scale = " + Time.timeScale);
            Time.timeScale = 0;
            gamePaused = true;
            Level_PlayerUI_Control.ins.AnimationPauseGame();
        }
        else
        {
            Debug.Log("Unpausing Game, Time Scale = " + Time.timeScale);
            Time.timeScale = 1;
            gamePaused = false;
            Level_PlayerUI_Control.ins.AnimationUnpauseGame();
        }

        Debug.Log("Time.timeScale = " + Time.timeScale);
    }
    public void ResetValuesToDefault()
    {
        gameLost = false;
        gameWon = false;
        gamePaused = false;
        Time.timeScale = 1;
        Game_State.ins.pausingWindow.SetActive(false);
    }

    
}
