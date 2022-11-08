using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{

    //Flag used to tell if the object can be interacted with or not.
    public bool isInteractable = false;
    public string[] iSentences;
    public string answer;
    public bool hasTextField;
    public int sentenceBeforeTextField;
    public int sentenceBeforeWrongResponse;
    private GameObject dialog;
    private GameObject player;
    private Dialog d;
    public bool canTrueMove = true;
    private bool subInteractable = false;
    public bool justEnded = false;

    public List<AudioClip> talkingClips;

    // Start is called before the first frame update
    void Start()
    {
        dialog = GameObject.Find("DialogManager");
        player = GameObject.Find("Capsule");
    }

    void Update()
    {
        if (justEnded == true)
        {
            justEnded = false;
        }
        subInteractable = false;
        if (gameObject.transform.parent.gameObject.transform.childCount > 1)
        {
            if (gameObject.transform.parent.gameObject.transform.GetChild(1).gameObject.tag == "Sit")
            {
                if (gameObject.transform.parent.gameObject.transform.GetChild(1).GetComponent<PlayerSit>().isInteractable == true)
                {
                    subInteractable = true;
                }
            }
        }
        Dialog d = dialog.GetComponent<Dialog>();
        PlayerController p = player.GetComponent<PlayerController>();
        //Checks if the player is in the collider and also if the key is pressed.
        if(isInteractable && Input.GetKeyDown(KeyCode.Space) && !subInteractable)
        {
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            if (gameObject.tag == "Ghost")
            {
                d.caller = gameObject.transform.parent.gameObject;
                d.talking = talkingClips[Random.Range(0, talkingClips.Count)];
            }
            if (d.sentences.Length == 0)
            {
                p.canMove = false;
                d.sentences = iSentences;
                d.hasTextField = hasTextField;
                d.sentenceBeforeTextField = sentenceBeforeTextField;
                d.sentenceBeforeWrongResponse = sentenceBeforeWrongResponse;
                d.answer = answer;
                StartCoroutine(d.Type());
            }
            else if ((d.textDisplay.text == d.sentences[d.index]))
            {
                d.NextSentence();
                if (d.index == d.sentences.Length && canTrueMove == true)
                {
                    p.canMove = true;
                    justEnded = true;
                }
                canTrueMove = true;
            }
            else
            {
                d.currentTypingSpeed = d.fastTypingSpeed;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Compares the tag of the object entering this collider.
        if(other.gameObject.tag == "Player")
        {
            isInteractable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //compares the tag of the object exiting this collider.
        if(other.gameObject.tag == "Player")
        {
            isInteractable = false;
        }
    }
}
