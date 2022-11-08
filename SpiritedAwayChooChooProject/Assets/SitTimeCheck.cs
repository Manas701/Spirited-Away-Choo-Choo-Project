using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitTimeCheck : MonoBehaviour
{
    public float sitAmount;
    private bool hasSatAmount = false;
    private bool killingLoneliness = false;
    public string[] newSentences;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (killingLoneliness == false)
        {
            if (gameObject.transform.GetChild(1).GetComponent<PlayerSit>().sittingTime >= sitAmount)
            {
                hasSatAmount = true;
                gameObject.transform.GetChild(0).GetComponent<InteractableObject>().iSentences = newSentences;
            }
            if (hasSatAmount == true)
            {
                if (gameObject.transform.GetChild(0).GetComponent<InteractableObject>().justEnded == true)
                {
                    StartCoroutine(GameObject.Find("DialogManager").GetComponent<Dialog>().QuestEnd());
                    killingLoneliness = true;
                }
            }
        }
    }
}
