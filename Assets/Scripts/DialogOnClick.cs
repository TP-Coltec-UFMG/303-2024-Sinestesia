using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogOnClick : MonoBehaviour
{
    public GameObject botao;
    public TMP_Text dialogText;

    public void dialog()
    {
        switch (botao.name) {
            case "firstPersonButton":
                dialogText.text = "Gostaria de uma música enérgica hoje!!";
                break;

        }
    }
}
