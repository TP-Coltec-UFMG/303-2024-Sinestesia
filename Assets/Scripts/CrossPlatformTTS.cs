using UnityEngine;
using System.Diagnostics;
using System.IO;
using System.Collections;
using UnityEngine.Networking;

public class CrossPlatformTTS : MonoBehaviour
{
    public string language = "pt";
    private string espeakPath;

    void Start()
    {
        #if UNITY_STANDALONE_WIN
            espeakPath = Path.Combine(Application.dataPath, "Plugins/eSpeakNG/Windows/eSpeak NG/espeak-ng.exe");
        #elif UNITY_STANDALONE_LINUX
            espeakPath = Path.Combine(Application.dataPath, "Plugins/eSpeakNG/Linux/espeak-ng");
        #endif
    }

    public void PlaySpeech(string text)
    {
        StartCoroutine(SynthesizeSpeech(text));
    }

    IEnumerator SynthesizeSpeech(string text)
    {
        string fileName = text.GetHashCode() + ".wav";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        if (!File.Exists(filePath))
        {
            GenerateSpeechFile(text, filePath);
        }

        // Usando UnityWebRequest para carregar o arquivo de áudio
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + filePath, AudioType.WAV))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                UnityEngine.Debug.Log(www.error);
            }
            else
            {
                AudioClip audioClip = DownloadHandlerAudioClip.GetContent(www);
                PlayAudioClip(audioClip);
            }
        }
    }

    void GenerateSpeechFile(string text, string filePath)
    {
        Process process = new Process();
        process.StartInfo.FileName = espeakPath;
        process.StartInfo.Arguments = $"-v {language} -w \"{filePath}\" \"{text}\"";
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.Start();
        process.WaitForExit();
    }

    void PlayAudioClip(AudioClip clip)
    {
        AudioSource audioSource = AudioManager.instance.ttsSource;
        audioSource.clip = clip;
        audioSource.Play();
    }
}
