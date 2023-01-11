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

    public Player_Manager playerManager;
    

    public Camera_Following cameraFollowing;
    public PlayerInputManager playerInputManager;
    public Level_GameOver gameOver;
    [Space(10)]
    public Image player1HealthBar;
    public Image player2HealthBar;
    public GameObject pauseWindow;
    public GameObject deathScreen;
    public Level_Ressurection resurrection;
    [Header("Abilities")]
    public Image player1Ability1;
    public Image player1Ability2;
    public Image player2Ability1;
    public Image player2Ability2;
}
