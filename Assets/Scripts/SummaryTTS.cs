using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummaryTTS : MonoBehaviour
{
    public CrossPlatformTTS tts;
    public Musician player;
    public Crowd crowd;
    private string textToSpeak;

    public void FalarSumario()
    {
        textToSpeak = "Pontos feitos, " + player.pontos + "e agrado com a plateia, " + crowd.agrado;
        tts.PlaySpeech(textToSpeak);
    }
}
