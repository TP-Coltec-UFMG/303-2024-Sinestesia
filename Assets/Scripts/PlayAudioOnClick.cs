using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayAudioOnClick : MonoBehaviour
{
    public GameObject botaoClicado;
    public void musicClick()
    {
        if (AudioManager.instance.sfxSource != null && AudioManager.instance.sfxSource.isPlaying)
        {
            AudioManager.instance.sfxSource.Stop();
        }
        switch (botaoClicado.name)
        {
            case "popFelizButton":
                AudioManager.instance.PlaySFX("popFelizClip");
                break;
            case "popTristeButton":
                AudioManager.instance.PlaySFX("popTristeClip");
                break;
            case "popRaivosoButton":
                AudioManager.instance.PlaySFX("popRaivosoClip");
                break;
            case "bluesFelizButton":
                AudioManager.instance.PlaySFX("bluesFelizClip");
                break;
            case "bluesRaivosoButton":
                AudioManager.instance.PlaySFX("bluesRaivosoClip");
                break;
            case "bluesTristeButton":
                AudioManager.instance.PlaySFX("bluesTristeClip");
                break;
        }
    }
}
