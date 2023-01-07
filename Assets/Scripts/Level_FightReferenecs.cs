using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Player_Manager playerManager;
    

    public Image player1HealthBar;
    public Image player2HealthBar;
    public Camera_Following cameraFollowing;
}
