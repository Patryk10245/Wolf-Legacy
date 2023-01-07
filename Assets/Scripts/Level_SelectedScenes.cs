using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_SelectedScenes : MonoBehaviour
{
    public static Level_SelectedScenes ins;
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
            //Debug.Log("Village scene");
        }
        else if(scene == SceneManager.GetSceneByName("Main_Menu"))
        {
            //Debug.Log("Menu Scene");
        }
        else
        {
            //Debug.Log("Fight Map Scene");
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
        Debug.Log("scene = " + currentName);
    }

    public void ChangeToVillageScene()
    {
        foreach(Player player in Player_Manager.ins.playerList)
        {
            player.controller.RemoveListeningOnEvents();
        }
        SceneManager.LoadScene("Village_Scene");
        
    }
    public void ChangeToMap1()
    {
        SceneManager.LoadScene("TESTING_SCENE");
    }
    public void ChangeToMap2()
    {
        SceneManager.LoadScene("TESTING_SCENE");
    }
    public void ChangeToMap3()
    {
        SceneManager.LoadScene("TESTING_SCENE");
    }
    public void ChangeToMainmenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}