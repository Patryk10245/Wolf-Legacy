using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_PlayerUI_Control : MonoBehaviour
{
    public static Level_PlayerUI_Control ins;
    [Header("References")]
    public Animator canvasAnimator;
    public Game_CanvasController canvasController;
    [Header("Windows")]
    public GameObject pauseWindow;
    [SerializeField] GameObject deathWindow;
    [SerializeField] GameObject completedWindow;
    [SerializeField] GameObject soundWindow;
    [Header("GoldFields")]
    [SerializeField] float updateSpeed = 0.05f;
    [SerializeField] int lastGoldValue;
    [SerializeField] Text levelCompletedGoldText;
    [SerializeField] Text gameOverGoldText;
    public Text pauseGameGoldText;
    [Header("Audio Sliders")]
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider soundSlider;

    [Header("Player 1 UI")]
    public GameObject player1UI;
    public Image player1HealthBar;
    public Image player1EnergyBar;
    [Space(5)]
    public Image player1Ability1;
    public Image player1Ability2;
    [Header("Player 2 UI")]
    public GameObject player2UI;
    public Image player2HealthBar;
    public Image player2EnergyBar;
    [Space(5)]
    public Image player2Ability1;
    public Image player2Ability2;

    private void Awake()
    {
        ins = this;
    }
    public void AnimationPauseGame()
    {
        Debug.Log("Starting animation");
        soundWindow.SetActive(false);
        canvasAnimator.SetTrigger("pauseGame");
    }
    public void AnimationUnpauseGame()
    {
        soundWindow.SetActive(false);
        canvasAnimator.SetTrigger("unpauseGame");
    }
    public void AnimationLevelCompleted()
    {
        canvasAnimator.SetTrigger("levelCompleted");
    }
    public void AnimationGameOver()
    {
        canvasAnimator.SetTrigger("gameOver");
    }

    public void AnimationEvent_GameOver_AnimateGold()
    {
        BeginAnimatingGold(gameOverGoldText);
    }
    public void AnimationEvent_LevelCompleted_AnimateGold()
    {
        BeginAnimatingGold(levelCompletedGoldText);
    }
    public void BeginAnimatingGold(Text text)
    {
        lastGoldValue = 0;

        StartCoroutine(AnimateText(text));
    }
    IEnumerator AnimateText(Text text)
    {
        while (lastGoldValue <= ScoreTable.ins.currentlyCollectedGold)
        {
            text.text = lastGoldValue.ToString();
            lastGoldValue++;

            yield return new WaitForSecondsRealtime(updateSpeed);
        }
    }


    public void UI_ReturnToVillageAfterDeath()
    {
        Level_FightReferenecs.ins.playerManager.DestroyAllPlayers();
        ScoreTable.ins.ReduceCollectedGoldByDeath();
        ScoreTable.ins.ApplyCollectedGold();
        GameSetup.ins.SaveClassData();
        Level_SelectedScenes.ins.ChangeToVillageScene();
    }
    public void UI_ReturnToMenuAfterDeath()
    {
        Level_FightReferenecs.ins.playerManager.DestroyAllPlayers();
        ScoreTable.ins.ReduceCollectedGoldByDeath();
        ScoreTable.ins.ApplyCollectedGold();
        GameSetup.ins.SaveClassData();
        Level_SelectedScenes.ins.ChangeToMainmenu();
    }
    public void UI_ReturnToVillage()
    {
        Level_FightReferenecs.ins.playerManager.DestroyAllPlayers();
        ScoreTable.ins.ApplyCollectedGold();
        GameSetup.ins.SaveClassData();
        Level_SelectedScenes.ins.ChangeToVillageScene();
    }
    public void UI_ReturnToMenu()
    {
        Level_FightReferenecs.ins.playerManager.DestroyAllPlayers();
        ScoreTable.ins.ApplyCollectedGold();
        GameSetup.ins.SaveClassData();
        Level_SelectedScenes.ins.ChangeToMainmenu();
    }

    public void UI_LoadNextMap()
    {
        foreach (Player player in Player_Manager.ins.playerList)
        {
            player.controller.RemoveListeningOnEvents();
        }


        Level_FightReferenecs.ins.playerManager.DestroyAllPlayers();
        ScoreTable.ins.ApplyCollectedGold();
        GameSetup.ins.SaveClassData();
        Level_SelectedScenes.ins.LoadNextScene();
    }

    // ================================================== SOUNDS ========================================

    public void ChangeMusicVolume()
    {
        AudioManager.ins.ChangeMusicVolume(musicSlider.value);
    }
    public void ChangeSoundVolume()
    {
        AudioManager.ins.ChangeSoundVolume(soundSlider.value);
    }
    public void CloseSoundWindow()
    {
        soundWindow.SetActive(false);
    }
    public void ShowSoundWindow()
    {
        musicSlider.value = AudioManager.ins.GetMusicVolume();
        soundSlider.value = AudioManager.ins.GetSoundVolume();
        soundWindow.SetActive(true);
    }

    // ================================================== DEATH ==========================================
    public void InitiateGameOver()
    {
        Game_State.gamePaused = true;
        Time.timeScale = 0;
        AnimationGameOver();
        //ScoreTable.ins.ApplyCollectedGold();
    }


    // ================================================== LEVEL COMPLETED ================================

    public void ShowLevelCompletedScreen()
    {
        if (Level_FightReferenecs.ins.isLastMap == true)
        {
            Game_State.gameWon = true;
        }
        completedWindow.gameObject.SetActive(true);
        AnimationLevelCompleted();
    }

    

}
