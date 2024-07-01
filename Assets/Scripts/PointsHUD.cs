using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsHUD : MonoBehaviour
{
    public TMP_Text pointsText;

    public void SetHUD(Musician player)
    {
        pointsText.text = "Pontos: " + player.pontos;
    } 
}
