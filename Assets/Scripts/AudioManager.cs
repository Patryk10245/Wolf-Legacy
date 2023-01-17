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
    [SerializeField] AudioSource source;
    [SerializeField] AudioSource playerHurtSource;
    public float masterVolume;

    [Header("Clips")]
    [SerializeField] AudioClip backgroundMusic;
    [SerializeField] AudioClip villageMusic;
    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioClip swordSlash;
    [SerializeField] AudioClip playerHurt;
    [SerializeField] AudioClip enemyHurt;

    


    private void Update()
    {
    }

    public void ChangeVolume(float volume)
    {
        source.volume = volume;
    }

    public void Play_MenuMusic()
    {
        source.clip = menuMusic;
        source.Play();
    }
    public void Play_VillageMusic()
    {
        source.clip = villageMusic;
        source.Play();
    }
    public void Play_GameMusic()
    {
        source.clip = backgroundMusic;
        source.Play();
    }

    public void Play_SwordSlash()
    {
        source.PlayOneShot(swordSlash);
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
        source.PlayOneShot(enemyHurt);
    }
}
