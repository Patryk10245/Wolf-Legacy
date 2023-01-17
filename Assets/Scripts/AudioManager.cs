using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager ins;
    void Reference()
    {
        ins = this;
    }
    private void Awake()
    {
        if (ins != null && ins != this)
        {

            Destroy(gameObject);
        }
        else
        {
            Reference();
            DontDestroyOnLoad(this);
        }
    }

    public AudioClip backgroundMusic;
    public AudioClip villageMusic;
    public AudioClip menuMusic;
    public AudioSource source;
    public float masterVolume;


    public void ChangeVolume(float volume)
    {
        source.volume = volume;
    }

    public void PlayMenuMusic()
    {
        source.clip = menuMusic;
        source.Play();
    }
    public void PlayVillageMusic()
    {
        source.clip = villageMusic;
        source.Play();
    }
    public void PlayGameMusic()
    {
        source.clip = backgroundMusic;
        source.Play();
    }
}
