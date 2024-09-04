using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreManager : MonoBehaviour
{
    public CrossPlatformTTS tts;
    public Toggle guitarra, bateria, teclado;
    public TMP_Text messageText;

    private void Start()
    {
        messageText.text = "Pontos do Músico: " + Musician.Musico.totalPontos;
    }

    public void CalculaPontos()
    {
        var pontos = Musician.Musico.totalPontos;

        var total = Avancar();

        var diferenca = Musician.Musico.totalPontos - total;

        if (Musician.Musico.totalPontos < 10)
        {
            messageText.text = "Não posso comprar nada... :(";
        }
        else if (total <= Musician.Musico.totalPontos)
        {
            messageText.text = "Pontos do Músico: " + diferenca;
        }
        else
        {
            messageText.text = "Não posso comprar isso...";
        }
    }

    public int Avancar()
    {
        var total = 0;

        if (guitarra.isOn)
            total += 10;
        if (teclado.isOn)
            total += 12;
        if (bateria.isOn)
            total += 20;

        Debug.Log("Total de pontos gastos: " + total);

        return total;
    }

    public void FalarAoSelecionar (Toggle toggle)
    {
        if (toggle.isOn)
        {
            tts.PlaySpeech("Habilitado");
        }
        else
        {
            tts.PlaySpeech("Desabilitado");
        }
    }

}
