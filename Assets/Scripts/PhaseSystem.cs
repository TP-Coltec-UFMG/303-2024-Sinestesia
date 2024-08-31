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

    private bool isPlayingMusic = false; //verificar se a música está tocando
    private bool isPlayingSpeech = false; //verificar se a fala da pessoa está sendo reproduzida

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
        //verifica se a música ou o áudio gravado terminou de tocar
        if (isPlayingMusic && !AudioManager.instance.sfxSource.isPlaying)
        {
            painelMusicasTocar.SetActive(true);
            isPlayingMusic = false;
        }

        if (isPlayingSpeech && !AudioManager.instance.sfxSource.isPlaying)
        {
            painelMusicasTocar.SetActive(true);
            isPlayingMusic = false;
        }
    }

    //métodos para ativar e desativar a "caixa" com as opções
    public void PlayingMusic()
    {
        if (!isPlayingMusic)
        {
            painelMusicasTocar.SetActive(false);
            isPlayingMusic = true;
        }
    }

    public void PlayingSpeech()
    {
        if (!isPlayingSpeech)
        {
            painelPlateia.SetActive(false);
            isPlayingSpeech = true;
        }
    }

    //configura qual é a música tema correta de cada fase
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

    //lógica do sistema de pontuação e de agrado da plateia
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

    public void AbrirMenuSinestesia()
    {
        SceneManager.LoadScene("menuSinestesia", LoadSceneMode.Additive);
    }

    public void sairParaMenu()
    {
        SceneManager.LoadScene("menuSinestesia");
        AudioManager.instance.musicSource.Play();
    }

}