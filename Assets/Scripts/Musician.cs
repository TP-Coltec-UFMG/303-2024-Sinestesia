using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musician : MonoBehaviour
{
    public static Musician Musico;

    [Range(0f, 4f)]
    public int consultaPlateia;
    public int pontos = 0;
    public int totalPontos;

    private void Awake()
    {
        if (Musico == null)
        {
            Musico = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
