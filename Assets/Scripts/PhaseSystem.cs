using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Net.WebSockets;
using System;
using UnityEngine.UI;

public enum PhaseState { START, PLAYERTURN, CROWDTURN, WIN, LOSS }

public class PhaseSystem : MonoBehaviour
{
    public PointsHUD pointsHUD;
    public AgradoHUD agradoHUD;

    public TMP_Text dialogueText;
    public TMP_Text nameText;

    public Musician player;
    public Crowd crowd;

    public GameObject painelPrincipal;
    public GameObject painelMusicasTocar;
    public GameObject painelPlateia;
    public GameObject painelMusical;
    public GameObject painelBotoes;
    public GameObject painelOpcoes;
    public GameObject painelFim_fase;


    public GameObject botaoPlateiaSelecionado;
    public GameObject botaoTocarSelecionado;
    public GameObject botaoSairDeTocar;
    public GameObject toggleLegendas;
    public GameObject botaoAvancar;

    private string musicaCorreta;

    private bool isPlayingMusic = false; //verificar se a m�sica est� tocando
    private bool isPlayingSpeech = false; //verificar se a fala da pessoa est� sendo reproduzida
    private bool isMusic = false; // verificar se a musica tocada e a correta da fase

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
        // verifica se a m�sica ou o �udio gravado terminou de tocar
        if (isPlayingMusic && !AudioManager.instance.sfxSource.isPlaying && isMusic == false)
        {
            Debug.Log(isMusic);

            painelPrincipal.SetActive(true);
            painelMusical.SetActive(false);
            isPlayingMusic = false;

            dialogueText.text = "O que será que eu vou tocar hoje?";
            nameText.text = "Eu:";

            EventSystem.current.SetSelectedGameObject(botaoSairDeTocar);
        }

        if (isPlayingMusic && !AudioManager.instance.sfxSource.isPlaying && isMusic == true)
        {
            Debug.Log(isMusic);
            
            painelFim_fase.SetActive(true);
            painelMusical.SetActive(false);
            painelBotoes.SetActive(false);    
            isPlayingMusic = false;

            EventSystem.current.SetSelectedGameObject(botaoAvancar);

        }

        if (isPlayingSpeech && !AudioManager.instance.sfxSource.isPlaying)
        {
            painelPrincipal.SetActive(true);
            painelBotoes.SetActive(true);
            isPlayingSpeech = false;

            dialogueText.text = "O que será que eu vou tocar hoje?";
            nameText.text = "Eu:";

            EventSystem.current.SetSelectedGameObject(botaoSairDeTocar);
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
            painelPrincipal.SetActive(true);
            painelBotoes.SetActive(false);
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
        } else if (currentSceneName == "Fase4")
        {
            musicaCorreta = "bluesTristeButton";
        }
    }

    void SetupNight()
    {
        dialogueText.text = "O que será que eu vou tocar hoje?";
        nameText.text = "Eu:";

        pointsHUD.SetHUD();
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
        // criando uma unica instancia do personagem para nao resetar os pontos a cada fase
        var nome = botao.name;
        if (nome == musicaCorreta)
        {
            Musician.Musico.pontos = Convert.ToInt32(20 / (Musician.Musico.consultaPlateia + 1));
            crowd.agrado += 3;

            if (crowd.agrado >= 10)
                crowd.agrado = 10;

            Musician.Musico.totalPontos += Musician.Musico.pontos;

            pointsHUD.pointsText.text = "Pontos: " + Musician.Musico.totalPontos;
            agradoHUD.agradoText.text = "😀 Agrado: " + crowd.agrado;

            Debug.Log(Musician.Musico.totalPontos);
        } else {
            Musician.Musico.pontos = Convert.ToInt32((5 * (Musician.Musico.consultaPlateia + 1)));

            Musician.Musico.totalPontos -= Musician.Musico.pontos;

            if (Musician.Musico.totalPontos <= 0)
            {
                Musician.Musico.totalPontos = 0;
            }
            crowd.agrado -= 2;
            pointsHUD.pointsText.text = "Pontos: " + Musician.Musico.totalPontos;
            agradoHUD.agradoText.text = "\U0001F614 Agrado: " + crowd.agrado;

            Debug.Log(Musician.Musico.totalPontos);
        }
    }

    // Calcula quantas consultas à plateia o jogador fez, interferindo na pontuação do jogador
    public void ConsultaSystem(GameObject botao)
    {
        var nome = botao.name;

        if (nome == "firstPersonButton" || nome == "secondPersonButton" || nome == "thirdPersonButton" || nome == "fourthPersonButton")
        {
            Musician.Musico.consultaPlateia++;
            Debug.Log("Consultas da plateia: " + Musician.Musico.consultaPlateia);
        }
    }

    public void VerificaMusica(GameObject botao) 
    {
        var nome = botao.name;
        if (nome == musicaCorreta) 
        {
            isMusic = true;
        }
    }

    public void AbrirMenuSinestesia()
    {
        painelOpcoes.SetActive(true);
        EventSystem.current.SetSelectedGameObject(toggleLegendas); // botão selecionado quando abre o painel
        //Aberração para abrir o menu de opções
    }

    public void FecharMenuSinestesia()
    {
        painelOpcoes.SetActive(false);
        EventSystem.current.SetSelectedGameObject(botaoSairDeTocar); //botão selecionado quando fecha o painel
    }

    public void AtivarAudiodescricao()
    {
        AudioManager.instance.ttsSource.mute = !AudioManager.instance.ttsSource.mute;
    }
    public void sairParaMenu()
    {
        SceneManager.LoadScene("menuSinestesia");
        AudioManager.instance.musicSource.Play();
    }
}