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
            Game_State.ins.pausingWindow.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Game_State.ins.pausingWindow.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void ResetValuesToDefault()
    {
        gameLost = false;
        gameLost = false;
        gamePaused = false;
        Time.timeScale = 1;
        Game_State.ins.pausingWindow.SetActive(false);
    }

    
}
