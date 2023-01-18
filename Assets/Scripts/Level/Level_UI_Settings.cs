using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_UI_Settings : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider soundSlider;
    public GameObject soundWindow;

    public void ChangeMusicVolume()
    {
        AudioManager.ins.ChangeMusicVolume(musicSlider.value);
    }
    public void ChangeSoundVolume()
    {
        AudioManager.ins.ChangeSoundVolume(soundSlider.value);
    }
    public void CloseWindow()
    {
        soundWindow.SetActive(false);
    }
    public void ShowWindow()
    {
        musicSlider.value = AudioManager.ins.GetMusicVolume();
        soundSlider.value = AudioManager.ins.GetSoundVolume();
        soundWindow.SetActive(true);
    }
}
