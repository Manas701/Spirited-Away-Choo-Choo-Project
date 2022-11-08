using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolaroidFlag : MonoBehaviour
{   

    private GameObject polaroid;
    private bool gotPackage = false;
    private bool killedForgetfulness = false;
    public string[] newSentences;

    // Start is called before the first frame update
    void Start()
    {
        polaroid = GameObject.Find("LockedPackage");
    }

    // Update is called once per frame
    void Update()
    {
        if (killedForgetfulness == false)
        {
            if (polaroid == null)
            {
                gotPackage = true;
                gameObject.transform.GetChild(0).GetComponent<InteractableObject>().iSentences = newSentences;
            }
            if (gotPackage == true)
            {
                if (gameObject.transform.GetChild(0).GetComponent<InteractableObject>().justEnded == true)
                {
                    StartCoroutine(GameObject.Find("DialogManager").GetComponent<Dialog>().QuestEnd());
                    killedForgetfulness = true;
                }
            }
        }
    }
}
