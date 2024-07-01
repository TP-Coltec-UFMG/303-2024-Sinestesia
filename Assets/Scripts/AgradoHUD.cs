using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AgradoHUD : MonoBehaviour
{
    public TMP_Text agradoText;

    public void SetHUD(Crowd crowd)
    {
        agradoText.text = "Agrado: " + crowd.agrado;  
    }
}
