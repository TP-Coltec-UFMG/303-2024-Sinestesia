using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Net.WebSockets;
using System;

public enum PhaseState { START, PLAYERTURN, CROWDTURN, WIN, LOSS }

public class PhaseSystem : MonoBehaviour
{
    public PointsHUD pointsHUD;
    public AgradoHUD agradoHUD;

    public TMP_Text dialogText;

    public Musician player;
    public Crowd crowd;

    public GameObject painelPrincipal;
    public GameObject painelMusicasTocar;
    public GameObject painelPlateia;

    public GameObject botaoPlateiaSelecionado;
    public GameObject botaoTocarSelecionado;
    public GameObject botaoSairDeTocar;
    public GameObject botaoClicado;

    public string musicaCorreta;

    public PhaseState state;

    // Start is called before the first frame update
    void Start()
    {
        state = PhaseState.START;
        SetupNight();
        VerifyScene();
    }

    void VerifyScene()
    {
        var currentScene = SceneManager.GetActiveScene();
        var currentSceneName = currentScene.name;
        if (currentSceneName == "Fase1")
        {
            musicaCorreta = "PopFelizButton";
        } else if (currentSceneName == "Fase2")
        {
            musicaCorreta = "PopTristeButton";
        } else if(currentSceneName == "Fase3")
        {
            musicaCorreta = "PopRaivosoButton";
        }
    }

    void SetupNight()
    {
        dialogText.text = "O que vamos tocar hoje?";

        pointsHUD.SetHUD(player);
        agradoHUD.SetHUD(crowd);
    }

    public void EnableMusicOptions()
    {
        state = PhaseState.PLAYERTURN;
        painelPrincipal.SetActive(false);
        painelMusicasTocar.SetActive(true);
        EventSystem.current.SetSelectedGameObject(botaoTocarSelecionado);
    }

    public void EnableCrowdOptions()
    {
        state = PhaseState.CROWDTURN;
        painelPrincipal.SetActive(false);
        painelPlateia.SetActive(true);
        EventSystem.current.SetSelectedGameObject(botaoPlateiaSelecionado);
    }

    public void EnableMainOptions()
    {
        state = PhaseState.PLAYERTURN;
        painelMusicasTocar.SetActive(false);
        painelPlateia.SetActive(false);
        painelPrincipal.SetActive(true);
        EventSystem.current.SetSelectedGameObject(botaoSairDeTocar);
    }
    public void pointsSystem()
    {
        if (botaoClicado.name == musicaCorreta)
        {
            player.pontos = Convert.ToInt32(player.pontos + (10 / player.consultaPlateia));
            crowd.agrado = 10;
        } else {
            player.pontos = Convert.ToInt32(player.pontos - 5 + (10 / player.consultaPlateia));
            crowd.agrado -= 2;
        }
    }

    public void sairParaMenu()
    {
        SceneManager.LoadScene("menuSinestesia");
        AudioManager.instance.musicSource.Play();
    }

}