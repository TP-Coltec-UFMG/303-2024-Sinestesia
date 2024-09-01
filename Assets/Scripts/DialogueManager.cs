using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    private Queue<string> sentences;

    public AudioClip[] dialogueClips;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences) 
        { 
            sentences.Enqueue(sentence); 
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0) 
        {
            EndDialogue();
            return;
        }

        if (AudioManager.instance.sfxSource != null && AudioManager.instance.sfxSource.isPlaying)
        {
            AudioManager.instance.sfxSource.Stop();
        }

        if (sentences.Count == 3)
        {
            AudioManager.instance.PlaySFX("falaIntro1");
        }

        else if (sentences.Count == 2)
        {
            AudioManager.instance.PlaySFX("falaIntro2");
        }

        else if (sentences.Count == 1)
        {
            AudioManager.instance.PlaySFX("falaIntro3");
        }

        Debug.Log(sentences.Count);

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.07f);
        }
    }

    public AudioClip GetAudioClip(int index)
    {
        if (index >= 0 && index < dialogueClips.Length)
        {
            return dialogueClips[index];
        }
        else
        {
            Debug.LogWarning("Índice de áudio inválido!");
            return null;
        }
    }
    void EndDialogue()
    {
        Debug.Log("End of conversation...");
    }

    public void NextScene()
    {
        if (sentences.Count == 0 && !AudioManager.instance.sfxSource.isPlaying) 
        {
            SceneManager.LoadScene("Fase1");
        }
    }
}
