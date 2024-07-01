using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class ListenButton : MonoBehaviour
{
    public Button btn;

    private void Awake()
    {
        btn.onClick.AddListener(VerifyButton);
    }

    public void VerifyButton()
    {
        switch (btn.name) {
            case "popFelizButton":
                AudioManager.instance.PlaySFX("popFelizClip");
                break;
            case "popTristeButton":
                AudioManager.instance.PlaySFX("popTristeClip");
                break;
            case "popRaivosoButton":
                AudioManager.instance.PlaySFX("popRaivosoClip");
                break;
        }
    }
}
