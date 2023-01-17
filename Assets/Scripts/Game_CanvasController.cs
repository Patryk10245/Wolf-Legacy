using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_CanvasController : MonoBehaviour
{
    public static Game_CanvasController ins;
    private void Awake()
    {
        ins = this;
    }

    public Level_FightReferenecs references;
    public Animator anim;

    public Text levelCompletedText;
    public Text gameOverText;
    
    public void PauseGame()
    {
        anim.SetTrigger("pauseGame");
    }
    public void UnpauseGame()
    {
        anim.SetTrigger("unpauseGame");
    }
    public void DeathScreen()
    {
        anim.SetTrigger("gameOver");
    }
    public void LevelCompleted()
    {
        anim.SetTrigger("levelCompleted");
    }

    public void AnimationEvent_GameOver_AnimateGold()
    {
        ScoreTable.ins.BeginAnimatingGold(gameOverText);
    }
    public void AnimationEvent_LevelCompleted_AnimateGold()
    {
        ScoreTable.ins.BeginAnimatingGold(levelCompletedText);
    }


}
