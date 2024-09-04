using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fim_Jogo : MonoBehaviour
{
    public GameObject painelFim_jogo;

    public void FimJogo()
    {
        painelFim_jogo.SetActive(true);
    }

    public void SairJogo()
    {
        Debug.Log("Sair do jogo");
        Application.Quit();
    }
}
