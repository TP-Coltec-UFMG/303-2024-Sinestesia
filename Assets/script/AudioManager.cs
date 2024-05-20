using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--------Audio Source--------")]
    [SerializeField] AudioSource musicSource;

    [Header("--------Audio Clip--------")]
    public AudioClip menu;
    
    private void Start() {
        musicSource.clip = menu;
        musicSource.Play();
    }
}
