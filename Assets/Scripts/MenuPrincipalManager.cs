using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuPrincipalManager : MonoBehaviour{

    [SerializeField] private string nomeDoLevelDeJogo;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelOpcoes;
    [SerializeField] private GameObject opcoesAbertoBotao, opcoesFechadoBotao;

    public void Jogar(){
    AudioManager.instance.musicSource.Stop();
    SceneManager.LoadScene("Fase1");
   }

   public void AbrirOpcoes(){
    painelMenuInicial.SetActive(false);
    painelOpcoes.SetActive(true);
    EventSystem.current.SetSelectedGameObject(null);
    EventSystem.current.SetSelectedGameObject(opcoesAbertoBotao);
    }

   public void FecharOpcoes(){
    painelOpcoes.SetActive(false);
    painelMenuInicial.SetActive(true);
    EventSystem.current.SetSelectedGameObject(null);
    EventSystem.current.SetSelectedGameObject(opcoesFechadoBotao);
    }


   public void SairJogo(){
    Debug.Log("Sair do jogo");
    Application.Quit();
   }
}

