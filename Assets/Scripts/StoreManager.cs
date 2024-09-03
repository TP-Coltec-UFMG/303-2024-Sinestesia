using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreManager : MonoBehaviour
{
    public Toggle guitarra, bateria, baixo;
    public Musician musico;
    public TMP_Text messageText;

    public void CalculaPontos()
    {
        var pontos = musico.pontos;
        if (musico.pontos < 10)
        {
            messageText.text = "Não posso comprar nada... :(";
        }
        else if (musico.pontos >= 10 && guitarra.isOn)
        {
            pontos -= 10;
            messageText.text = "Pontos: " + pontos;
            pontos += 10;
        }
        else if (musico.pontos >= 12 && baixo.isOn)
        {
            pontos -= 12;
            messageText.text = "Pontos: " + musico.pontos;
            pontos += 12;
        }
        else if (musico.pontos >= 20 && bateria.isOn)
        {
            pontos -= 20;
            messageText.text = "Pontos: " + pontos;
            pontos += 20;
        }
        else
        {
            messageText.text = "Não posso comprar isso...";
        }
    }

    public void Avancar()
    {
        var total = 0;

        if (guitarra.isOn)
            total += 10;
        if (baixo.isOn)
            total += 12;
        if (bateria.isOn)
            total += 20;

        Debug.Log("Total de pontos gastos: " + total);
    }

}
