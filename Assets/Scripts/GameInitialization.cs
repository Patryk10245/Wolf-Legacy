using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitialization : MonoBehaviour
{
    public static GameInitialization ins;
    public void Reference()
    {
        ins = this;
    }
    private void Awake()
    {
        Reference();
        DontDestroyOnLoad(this);
    }


    public Player_Manager playerManager;
    public Village_Upgrades villageUpgrades;
    public GameSetup gameSetup;
    public ScoreTable scoreTable;
    public Camera_Following cameraFollowing;
    public Level_SelectedScenes selectedScenes;


    void Start()
    {
        //PlayerPrefs.SetInt("int "1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
