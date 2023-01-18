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

    [Header("References")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource soundSource;
    [SerializeField] AudioSource playerHurtSource;

    [Header("Clips")]
    [SerializeField] AudioClip backgroundMusic;
    [SerializeField] AudioClip villageMusic;
    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioClip[] swordSlash;
    [SerializeField] AudioClip playerHurt;
    [SerializeField] AudioClip enemyHurt;

    


    private void Update()
    {
    }

    public void ChangeMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    public void ChangeSoundVolume(float volume)
    {
        soundSource.volume = volume;
    }

    public void Play_MenuMusic()
    {
        musicSource.clip = menuMusic;
        musicSource.Play();
    }
    public void Play_VillageMusic()
    {
        musicSource.clip = villageMusic;
        musicSource.Play();
    }
    public void Play_GameMusic()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void Play_SwordSlash()
    {
        int rand = Random.Range(0, 3);
        soundSource.PlayOneShot(swordSlash[rand]);
    }
    public void Play_PlayerHurt()
    {
        if(!playerHurtSource.isPlaying)
        {
            playerHurtSource.Play();
        }
    }
    public void Play_EnemyHurt()
    {
        soundSource.PlayOneShot(enemyHurt);
    }
}
