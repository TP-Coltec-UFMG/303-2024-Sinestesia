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

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        var currentScene = SceneManager.GetActiveScene();
        var currentSceneName = currentScene.name;

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
        var currentScene = SceneManager.GetActiveScene();
        var currentSceneName = currentScene.name;

        Debug.Log("A cena atual é: " + currentSceneName);

        if (sentences.Count == 0) 
        {
            EndDialogue();
            return;
        }

        if (AudioManager.instance.sfxSource != null && AudioManager.instance.sfxSource.isPlaying)
        {
            AudioManager.instance.sfxSource.Stop();
        }

        if (currentSceneName == "Intro")
        {
            switch (sentences.Count)
            {
                case 3:
                    AudioManager.instance.PlaySFX("falaIntro1");
                    break;
                case 2:
                    AudioManager.instance.PlaySFX("falaIntro2");
                    break;
                case 1:
                    AudioManager.instance.PlaySFX("falaIntro3");
                    break;
                default:
                    break;
            } 
        }

        if (currentSceneName == "Fase1")
        {
            switch (nameText.text)
            {
                case "vovo":
                    AudioManager.instance.PlaySFX("vovoFeliz");
                    break;
                case "jessie":
                    AudioManager.instance.PlaySFX("jessieFeliz");
                    break;
                case "explorer":
                    AudioManager.instance.PlaySFX("explorerFeliz");
                    break;
                case "deep":
                    AudioManager.instance.PlaySFX("deepFeliz");
                    break;
                default:
                    break;
            }
        }

        if (currentSceneName == "Fase4")
        {
            switch (nameText.text)
            {
                case "vovo":
                    AudioManager.instance.PlaySFX("vovoTriste");
                    break;
                case "jessie":
                    AudioManager.instance.PlaySFX("jessieTriste");
                    break;
                case "explorer":
                    AudioManager.instance.PlaySFX("explorerTriste");
                    break;
                case "deep":
                    AudioManager.instance.PlaySFX("deepTriste");
                    break;
                default:
                    break;
            }
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

    void EndDialogue()
    {
        Debug.Log("End of conversation...");
    }

    public void NextScene(int index)
    {
        if (sentences.Count == 0 && !AudioManager.instance.sfxSource.isPlaying) 
        {
            SceneManager.LoadScene(index + 1);
        }
    }
}
