using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{

    //Flag used to tell if the object can be interacted with or not.
    public bool isInteractable = false;
    public string[] iSentences;
    private GameObject dialog;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        dialog = GameObject.Find("DialogManager");
        player = GameObject.Find("Capsule");
    }

    void Update()
    {
        Dialog d = dialog.GetComponent<Dialog>();
        PlayerController p = player.GetComponent<PlayerController>();
        //Checks if the player is in the collider and also if the key is pressed.
        if(isInteractable && Input.GetKeyDown(KeyCode.Space))
        {
            if (d.sentences.Length == 0)
            {
                p.canMove = false;
                d.sentences = iSentences;
                StartCoroutine(d.Type());
            }
            else if ((d.textDisplay.text == d.sentences[d.index]))
            {
                d.NextSentence();
                if (d.index == d.sentences.Length)
                {
                    p.canMove = true;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        //Compares the tag of the object entering this collider.
        if(other.gameObject.tag == "Player")
        {
            isInteractable = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        //compares the tag of the object exiting this collider.
        if(other.gameObject.tag == "Player")
        {
            isInteractable = false;
        }
    }
}
