using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuPrincipalManager : MonoBehaviour
{
    [SerializeField] private string nomeDoLevelDeJogo;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelOpcoes;
    [SerializeField] private GameObject opcoesAbertoBotao, opcoesFechadoBotao;

    public void Start()
    {
        if (SceneManager.GetSceneByName("Fase1").isLoaded)
        {
            AbrirOpcoes();
        }
    }

    public void Jogar()
    {
    AudioManager.instance.musicSource.Stop();
    SceneManager.LoadScene("Intro");
   }

    public void AbrirOpcoes()
    {
        painelMenuInicial.SetActive(false);
        painelOpcoes.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(opcoesAbertoBotao);
    }

    public void FecharOpcoes()
    {
        painelOpcoes.SetActive(false);
        painelMenuInicial.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(opcoesFechadoBotao);

        if (SceneManager.GetSceneByName("Fase1").isLoaded)
        {
            SceneManager.UnloadSceneAsync("menuSinestesia");
        }
    }

    public void SairJogo()
    {
        Debug.Log("Sair do jogo");
        Application.Quit();
    }

    public void FecharMenuSinestesia()
    {
        // Descarrega a cena "menuSinestesia" e retorna � fase original
        SceneManager.UnloadSceneAsync("menuSinestesia");
    }
}