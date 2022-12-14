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
    private Fade blackOut;
    public GameObject caller;
    public float fadedTime;
    private PlayerController player;
    private GameObject obj;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Capsule").GetComponent<PlayerController>();
        blackOut = GameObject.Find("PlaneOfDoom").GetComponent<Fade>();
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
        if (s.ToLower().Replace(" ", "") == answer)
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
                if (hasResponded && answerCorrect)
                {
                    StartCoroutine(QuestEnd());
                }
                hasResponded = false;
                answerCorrect = false;
                talking = null;
            }
        }
    }

    public IEnumerator QuestEnd()
    {
        animator.SetBool("isOpen", false);
        blackOut.FadeBlack();
        for (int i = 0; i < caller.transform.childCount; i++)
        {
            obj = caller.transform.GetChild(i).gameObject;
            if (obj.tag == "Interact")
            {
                break;
            }
        }
        obj.gameObject.GetComponent<InteractableObject>().canTrueMove = false;
        obj.gameObject.SetActive(false);
        Destroy(obj.gameObject);
        player.canMove = false;
        yield return new WaitForSeconds(blackOut.fadeTime+fadedTime);
        Destroy(caller);
        blackOut.UnfadeBlack();
        player.canMove = true;
        obj = null;
    }
}
