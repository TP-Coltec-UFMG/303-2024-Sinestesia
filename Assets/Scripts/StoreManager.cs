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

        var total = Avancar();

        if (musico.pontos < 10)
        {
            messageText.text = "Não posso comprar nada... :(";
        }
        else if (total <= musico.pontos)
        {
            messageText.text = "Pontos: " + total;
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
        if (baixo.isOn)
            total += 12;
        if (bateria.isOn)
            total += 20;

        Debug.Log("Total de pontos gastos: " + total);

        return total;
    }

}
