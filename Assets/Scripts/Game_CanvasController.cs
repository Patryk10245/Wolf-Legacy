using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_CanvasController : MonoBehaviour
{
    public void AnimationEvent_GameOver_AnimateGold()
    {
        Level_PlayerUI_Control.ins.AnimationEvent_GameOver_AnimateGold();
    }
    public void AnimationEvent_LevelCompleted_AnimateGold()
    {
        Level_PlayerUI_Control.ins.AnimationEvent_LevelCompleted_AnimateGold();
    }
}
