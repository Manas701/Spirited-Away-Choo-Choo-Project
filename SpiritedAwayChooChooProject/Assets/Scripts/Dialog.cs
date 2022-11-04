using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    public string answer;
    public int index;
    public float typingSpeed;
    public float fastTypingSpeed;
    public float currentTypingSpeed;
    public TMP_InputField inputField;
    public GameObject inputVisible;
    public bool hasTextField;
    public int sentenceBeforeTextField;
    public int sentenceBeforeWrongResponse;
    private bool answerCorrect = false;
    private bool hasResponded = false;
    public AudioSource audioSource;
    public AudioClip talking;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("Talking Audio").GetComponent<AudioSource>();
        inputVisible.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator Type()
    {
        if (talking != null)
        {
            audioSource.clip = talking;
            audioSource.Play();
        }
        talking = null;
        currentTypingSpeed = typingSpeed;
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(currentTypingSpeed);
        }
    }

    public void endInputField(string s)
    {
        if (s == answer)
        {
            answerCorrect = true;
        }
        hasResponded = true;
        inputVisible.SetActive(false);
        inputField.DeactivateInputField();
        inputField.text = "";
        NextSentence();
    }

    // public void TextField()
    // {
    //     pass;
    // }

    public void NextSentence()
    {
        if (!hasResponded && hasTextField && (index ==  sentenceBeforeTextField - 1))
        {
            inputVisible.SetActive(true);
            inputField.ActivateInputField();
        }
        else
        {
            textDisplay.text = "";
            if (index < sentences.Length - 1 && !(hasResponded && (index == sentenceBeforeWrongResponse - 1) && answerCorrect))
            {
                index++;
                if (hasTextField && hasResponded && !answerCorrect && (index == sentenceBeforeTextField))
                {
                    index = sentenceBeforeWrongResponse;
                }
                StartCoroutine(Type());
            }
            else
            {
                sentences = new string[0];
                index = 0;
                hasResponded = false;
                answerCorrect = false;
                talking = null;
            }
        }
    }
}
