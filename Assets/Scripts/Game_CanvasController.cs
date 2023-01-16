using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_CanvasController : MonoBehaviour
{
    public static Game_CanvasController ins;
    private void Awake()
    {
        ins = this;
    }

    public Level_FightReferenecs references;
    public Animator anim;
    
    public void PauseGame()
    {
        anim.SetTrigger("pauseGame");
    }
    public void UnpauseGame()
    {
        anim.SetTrigger("unpauseGame");
    }

}
