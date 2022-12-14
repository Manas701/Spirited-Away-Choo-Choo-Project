using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSit : MonoBehaviour
{
    public bool isInteractable = false;
    public bool isSitting = false;
    private GameObject player;
    public float sitPosX;
    public float sitPosY;
    public float sitPosZ;
    public float sitRotX;
    public float sitRotY;
    public float sitRotZ;
    public float unsitPosX;
    public float unsitPosY;
    public float unsitPosZ;
    public float unsitRotX;
    public float unsitRotY;
    public float unsitRotZ;
    public float tweenTime;
    public float sittingTime = 0f;
    private int numGhosts;
    private bool cantGetUp = false;
    private bool creditsActivated = false;
    public float neonSpeed;
    private GameObject neonSigns;
    private bool hasBeenDone = false;
    public float indicatorTime;
    private Image indicator;
    public Sprite spacebar;
    public Sprite spacebarPressed;
    public float indicatorTweenTime;
    private Color fixedColor;
    private float pressTime = 0f;
    private bool pressed = false;
    public float pressTimeWait;
    private bool turnOffSpace = true;

    // public GameObject[] Robert;
    // public GameObject[] Newlin;
    // public GameObject[] Michelle;
    // public GameObject[] Huang;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Capsule");
        neonSigns = GameObject.Find("Neon_Signs");
        indicator = GameObject.Find("Image").GetComponent<Image>();
        fixedColor = indicator.color;
        fixedColor.a = 0f;
        indicator.color = fixedColor;
        if (neonSigns != null)
        {
            neonSigns.SetActive(false);
        }
        if (isSitting)
        {
            player.transform.position = new Vector3(sitPosX, sitPosY, sitPosZ);
            player.transform.Rotate(new Vector3(sitRotX, sitRotY, sitRotZ));
        }
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().useGravity = false;
        player.GetComponent<Collider>().isTrigger = true;
        player.GetComponent<PlayerController>().canMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        numGhosts = GameObject.FindGameObjectsWithTag("Ghost").Length;
        if (numGhosts == 1)
        {
            // Overlay glowing seat
            cantGetUp = true;
        }
        if(isInteractable && Input.GetKeyDown(KeyCode.Space))
        {
            if (isSitting)
            {
                if (cantGetUp == false)
                {
                    Unsit();
                }
            }
            else
            {
                Sit();
            }
        }
        if (isSitting)
        {
            sittingTime += Time.deltaTime;
            if (turnOffSpace)
            {
                if (hasBeenDone)
                {
                    pressTime += Time.deltaTime;
                }
                if (!hasBeenDone && sittingTime >= indicatorTime)
                {
                    if (fixedColor.a <= 1f)
                    {
                        fixedColor.a += Time.deltaTime;
                        indicator.color = fixedColor;
                    }
                    else
                    {
                        hasBeenDone = true;
                    }
                }
                if (hasBeenDone && pressTime >= pressTimeWait)
                {
                    if (!pressed)
                    {
                        pressed = true;
                        indicator.overrideSprite = spacebarPressed;
                        pressTime = 0f;
                    }
                    else
                    {
                        pressed = false;
                        indicator.overrideSprite = null;
                        pressTime = 0f;
                    }
                }
            }
            if (cantGetUp == true && creditsActivated == false)
            {
                creditsActivated = true;
                neonSigns.SetActive(true);
                neonSigns.GetComponent<Rigidbody>().velocity = new Vector3(neonSpeed, 0, 0);
            }
        }
        else
        {
            sittingTime = 0f;
        }
    }

    public void Sit()
    {
        //move the player down and facing the window, kill movement
        LeanTween.move(player, new Vector3(sitPosX, sitPosY, sitPosZ), tweenTime).setEase(LeanTweenType.easeInOutCubic);
        player.transform.Rotate(new Vector3(sitRotX, sitRotY, sitRotZ));
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().useGravity = false;
        player.GetComponent<Collider>().isTrigger = true;
        player.GetComponent<PlayerController>().canMove = false;
        isSitting = true;
    }

    public void Unsit()
    {
        //move the player up and still facing the window, unkill movement
        if (turnOffSpace)
        {
            GameObject.Find("Image").SetActive(false);
        }
        hasBeenDone = true;
        turnOffSpace = false;
        LeanTween.move(player, new Vector3(unsitPosX, unsitPosY, unsitPosZ), tweenTime).setEase(LeanTweenType.easeInOutCubic);
        player.transform.Rotate(new Vector3(unsitRotX, unsitRotY, unsitRotZ));
        player.GetComponent<Rigidbody>().useGravity = true;
        player.GetComponent<Collider>().isTrigger = false;
        player.GetComponent<PlayerController>().canMove = true;
        isSitting = false;
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
