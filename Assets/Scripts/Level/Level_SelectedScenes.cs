using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_SelectedScenes : MonoBehaviour
{
    public static Level_SelectedScenes ins;
    public int currentFightScene = -1;
    [SerializeField]
    public string[] fightScenes;
    public void Reference()
    {
        ins = this;
    }

    private void Awake()
    {
        Reference();
        DontDestroyOnLoad(this);
    }




    private void Start()
    {
        SceneManager.activeSceneChanged += SceneChanged;
        SceneManager.sceneLoaded += SceneLoaded;
    }

    void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("OnSceneLoaded: " + scene.name);
        //Debug.Log(mode);

        if(scene == SceneManager.GetSceneByName("Village_Scene"))
        {
            
            Village_UI_Control.ins.SetReference();
            Village_Upgrades.ins.UpdateClassesUIUpgrades();
            ScoreTable.ins.ApplyCollectedGold();
            //Debug.Log("Village scene");
        }
        else if(scene == SceneManager.GetSceneByName("Main_Menu_Scene"))
        {
            //Debug.Log("Menu Scene");
        }
        else
        {
            Debug.Log("Scene changed to Fight Scene");
            GameSetup.ins.SetUpTheGame();
        }
    }
    void SceneChanged(Scene current, Scene next)
    {
        string currentName = current.name;
        //Debug.Log("Scene current = " + currentName + " \n next = " + next.name);

        if (currentName == null)
        {
            // Scene1 has been removed
            currentName = "Replaced";
        }
        
        //Debug.Log("scene = " + currentName);
    }

    public void ChangeToVillageScene()
    {
        foreach(Player player in Player_Manager.ins.playerList)
        {
            player.controller.RemoveListeningOnEvents();
        }
        SceneManager.LoadScene("Village_Scene");
        
    }


    public void RepeatFightMap()
    {

    }
    public void LoadNextFightMap()
    {
        currentFightScene++;
        if(currentFightScene == 1)
        {
            ChangeToMainmenu();
        }
        SceneManager.LoadScene(fightScenes[currentFightScene]);
    }
    public void ChangeToMap1()
    {
        SceneManager.LoadScene("Wolf Legacy copy");
    }
    public void ChangeToMap2()
    {
        SceneManager.LoadScene("Wolf Legacy copy");
    }
    public void ChangeToMap3()
    {
        SceneManager.LoadScene("Wolf Legacy copyE");
    }
    public void ChangeToMainmenu()
    {
        SceneManager.LoadScene("Main_Menu_Scene");
    }
    public void ChangeToTestingScene()
    {
        SceneManager.LoadScene("TESTING_SCENE");
    }
}
