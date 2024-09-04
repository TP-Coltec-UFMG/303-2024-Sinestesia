using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsHUD : MonoBehaviour
{
    public TMP_Text pointsText;

    public void SetHUD()
    {
        pointsText.text = "Pontos: " + Musician.Musico.totalPontos;
    } 
}
