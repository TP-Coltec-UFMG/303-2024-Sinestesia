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
    public GameObject painelMusical;

    public GameObject botaoPlateiaSelecionado;
    public GameObject botaoTocarSelecionado;
    public GameObject botaoSairDeTocar;
    public GameObject botaoClicado;

    public string musicaCorreta;

    private bool isPlayingMusic = false; //verificar se a m�sica est� tocando
    private bool isPlayingSpeech = false; //verificar se a fala da pessoa est� sendo reproduzida

    public PhaseState state;

    // Start is called before the first frame update
    void Start()
    {
        state = PhaseState.START;
        SetupNight();
        VerifyScene();
    }

    private void Update()
    {
        //verifica se a m�sica ou o �udio gravado terminou de tocar
        if (isPlayingMusic && !AudioManager.instance.sfxSource.isPlaying)
        {
            painelPrincipal.SetActive(true);
            painelMusical.SetActive(false);
            isPlayingMusic = false;
            EventSystem.current.SetSelectedGameObject(botaoSairDeTocar);
        }

        if (isPlayingSpeech && !AudioManager.instance.sfxSource.isPlaying)
        {
            painelPrincipal.SetActive(true);
            painelMusical.SetActive(false);
            isPlayingMusic = false;
        }
    }

    //m�todos para ativar e desativar a "caixa" com as op��es
    public void PlayingMusic()
    {
        if (!isPlayingMusic)
        {
            painelMusicasTocar.SetActive(false);
            painelMusical.SetActive(true);
            isPlayingMusic = true;
        }
    }

    public void PlayingSpeech()
    {
        if (!isPlayingSpeech)
        {
            painelPlateia.SetActive(false);
            painelMusical.SetActive(true);
            isPlayingSpeech = true;
        }
    }

    //configura qual � a m�sica tema correta de cada fase
    void VerifyScene()
    {
        var currentScene = SceneManager.GetActiveScene();
        var currentSceneName = currentScene.name;

        Debug.Log(currentSceneName);

        if (currentSceneName == "Fase1")
        {
            musicaCorreta = "popFelizButton";
        } else if (currentSceneName == "Fase2")
        {
            musicaCorreta = "popTristeButton";
        } else if(currentSceneName == "Fase3")
        {
            musicaCorreta = "popRaivosoButton";
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

    //l�gica do sistema de pontua��o e de agrado da plateia
    public void pointsSystem(GameObject botao)
    {
        var nome = botao.name;
        Debug.Log(nome);
        if (nome == musicaCorreta)
        {
            player.pontos = Convert.ToInt32(player.pontos + (10 / (player.consultaPlateia + 1)));
            crowd.agrado = 10;
            pointsHUD.pointsText.text = "Pontos: " + player.pontos;
            agradoHUD.agradoText.text = "Agrado: " + crowd.agrado;
        } else {
            player.pontos = Convert.ToInt32(player.pontos - 10 + (5 / (player.consultaPlateia + 1)));
            crowd.agrado -= 2;
            pointsHUD.pointsText.text = "Pontos: " + player.pontos;
            agradoHUD.agradoText.text = "Agrado: " + crowd.agrado;
        }
    }

    public void AbrirMenuSinestesia()
    {
        SceneManager.LoadScene("menuSinestesia", LoadSceneMode.Additive);
        //Aberração para abrir o menu de opções
    }

    public void sairParaMenu()
    {
        SceneManager.LoadScene("menuSinestesia");
        AudioManager.instance.musicSource.Play();
    }

}