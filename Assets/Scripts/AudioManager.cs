using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sfxSounds;

    [Header("--------Audio Source--------")]
    [SerializeField] public AudioSource musicSource;
    [SerializeField] public AudioSource sfxSource;
    [SerializeField] public AudioSource ttsSource;

    [Header("--------Audio Clip--------")]
    public AudioClip menu;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    private void Start() {
        if (menu == null)
            return;
        else
        {
            musicSource.clip = menu;
            musicSource.Play();
        }
    }
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Som não encontrado");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }
    public void ToggleSFX()
    {
        var currentScene = SceneManager.GetActiveScene();
        var currentSceneName = currentScene.name;

        if (currentSceneName == "menuSinestesia")
        {
            sfxSource.mute = !sfxSource.mute;
        }
        else
        {
            ttsSource.mute = !ttsSource.mute;
        }
    }
}
