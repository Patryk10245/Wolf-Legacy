using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Level_FightReferenecs : MonoBehaviour
{
    public static Level_FightReferenecs ins;
    public void Reference()
    {
        ins = this;
    }

    private void Awake()
    {
        Reference();
    }

    public bool isLastMap;

    public Player_Manager playerManager;

    public Level_PlayerUI_Control playerUIControl;
    public Camera_Following cameraFollowing;
    public PlayerInputManager playerInputManager;

    public Level_Ressurection resurrection;

}
