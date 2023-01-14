using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_State : MonoBehaviour
{
    public static Game_State ins;

    public static bool gamePaused;
    public static GameObject pausingWindow;
    public bool gameLost;
    public bool gameWon;

    // Start is called before the first frame update
    void Start()
    {
        ins = this;
        //DontDestroyOnLoad(this);
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
            pausingWindow.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pausingWindow.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void ResetValuesToDefault()
    {
        gameLost = false;
        gameLost = false;
        gamePaused = false;
        Time.timeScale = 1;
    }

    
}
