using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Net.WebSockets;
using System;
using UnityEngine.UI;

public class Fim_fase : MonoBehaviour
{
    public void NextFase()
    {
        if(SceneManager.GetSceneByName("Fase1").isLoaded)
        {
            SceneManager.LoadScene("Fase2");
            SceneManager.UnloadSceneAsync("Fase1");

        }if(SceneManager.GetSceneByName("Fase2").isLoaded)
        {
            SceneManager.LoadScene("Fase3");
            SceneManager.UnloadSceneAsync("Fase2");

        }if(SceneManager.GetSceneByName("Fase3").isLoaded)
        {
            SceneManager.LoadScene("Fase4");
            SceneManager.UnloadSceneAsync("Fase3");

        }if(SceneManager.GetSceneByName("Fase4").isLoaded)
        {
            SceneManager.LoadScene("Loja");
            SceneManager.UnloadSceneAsync("Fase4");
        }
    }
}
