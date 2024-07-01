using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnClick : MonoBehaviour
{
    public GameObject botaoClicado;
    public void musicClick()
    {
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
        }
    }
}
