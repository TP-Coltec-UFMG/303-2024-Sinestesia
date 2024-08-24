using UnityEngine;
using UnityEngine.EventSystems;

public class TTSOnSelect : MonoBehaviour, ISelectHandler
{
    public CrossPlatformTTS tts;
    public string textToSpeak = "This object is now selected.";

    public void OnSelect(BaseEventData eventData)
    {
        // Verifica se o script TTS está atribuído
        if (tts != null)
        {
            // Chama a função PlaySpeech passando o texto desejado
            tts.PlaySpeech(textToSpeak);
        }
        else
        {
            Debug.LogError("O script CrossPlatformTTS não está atribuído ao objeto.");
        }
    }
}
