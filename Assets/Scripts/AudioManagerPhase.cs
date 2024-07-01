using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerPhase : MonoBehaviour
{
    public static AudioManagerPhase instancePhase;

    public Sound[] sounds;

    [SerializeField] public AudioSource musicSource;
    [SerializeField] public AudioSource sfxSource;

    private void Awake()
    {
        if (instancePhase == null)
        {
            instancePhase = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Som não encontrado");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }


}
