using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public GameObject dialoguePanel;
    public GameObject contunueButton;
    public GameObject smallCanvas;

    private void Start()
    {
        StartCoroutine(Type());
    }

        // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && smallCanvas.activeInHierarchy == true)
        {
            dialoguePanel.SetActive(true);
        }

        if (textDisplay.text == sentences[index])
        {
            contunueButton.SetActive(true);
        }
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }

    public void DisplayNextSentence()
    {
        contunueButton.SetActive(false);
        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            dialoguePanel.SetActive(false);
        }
    }
}
