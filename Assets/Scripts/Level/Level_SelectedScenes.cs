using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Level_SelectedScenes : MonoBehaviour
{
    public static Level_SelectedScenes ins;
    public int currentFightScene = -1;

    public string[] selectedScenes;
    public void Reference()
    {
        ins = this;
    }

    private void Awake()
    {
        if(ins == null)
        {
            SceneManager.activeSceneChanged += SceneChanged;
            SceneManager.sceneLoaded += SceneLoaded;
        }

        if(ins != null && ins != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Reference();
            DontDestroyOnLoad(this);
        }
    }




    private void Start()
    {
        //Debug.Log("Level selected scenes start");
        //SceneManager.activeSceneChanged += SceneChanged;
        //SceneManager.sceneLoaded += SceneLoaded;
        
    }

    void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //SceneManager.sceneLoaded -= SceneLoaded;
        Debug.Log("OnSceneLoaded: " + scene.name);
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
            //Debug.Log("Scene changed to Fight Scene");
            GameSetup.ins.SetUpTheGame();
        }
    }
    void SceneChanged(Scene current, Scene next)
    {
        //SceneManager.activeSceneChanged -= SceneChanged;
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
    public void ChangeToMainmenu()
    {
        //Debug.Log("change to main menu");
        foreach (Player player in Player_Manager.ins.playerList)
        {
            player.controller.RemoveListeningOnEvents();
        }
        SceneManager.LoadScene("Main_Menu_Scene");
    }
    public void RepeatFightMap()
    {
        SceneManager.LoadScene(selectedScenes[currentFightScene]);
    }
    public void LoadNextScene()
    {
        currentFightScene++;

        if(currentFightScene >= selectedScenes.Length)
        {
            ChangeToMainmenu();
            return;
        }

        SceneManager.LoadScene(selectedScenes[currentFightScene]);
    }

    public void ChangeToTestingScene()
    {
        //Debug.Log("change to testing scene");
        SceneManager.LoadScene("TESTING_SCENE");
    }

}
