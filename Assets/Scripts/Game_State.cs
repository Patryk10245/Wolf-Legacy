using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_State : MonoBehaviour
{
    public static bool gamePaused;
    public static GameObject pausingWindow;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
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
            Time.timeScale = 0;
            pausingWindow.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pausingWindow.SetActive(false);
        }
    }
}
