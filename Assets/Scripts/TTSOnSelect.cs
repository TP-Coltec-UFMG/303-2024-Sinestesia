using UnityEngine;
using UnityEngine.EventSystems;

public class TTSOnSelect : MonoBehaviour, ISelectHandler
{
    public CrossPlatformTTS tts;
    public string textToSpeak = "This object is now selected.";

    public void OnSelect(BaseEventData eventData)
    {
        // Verifica se o script TTS est� atribu�do
        if (tts != null)
        {
            // Chama a fun��o PlaySpeech passando o texto desejado
            tts.PlaySpeech(textToSpeak);
        }
        else
        {
            Debug.LogError("O script CrossPlatformTTS n�o est� atribu�do ao objeto.");
        }
    }
}
