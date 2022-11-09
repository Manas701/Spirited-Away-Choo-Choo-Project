using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Capsule");
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
        if(isInteractable && Input.GetKeyDown(KeyCode.Space))
        {
            if (isSitting)
            {
                Unsit();
            }
            else
            {
                Sit();
            }
        }
        if (isSitting)
        {
            sittingTime += Time.deltaTime;
        }
        else
        {
            sittingTime = 0f;
        }
    }

    public void Sit()
    {
        //move the player down and facing the window, kill movement
        LeanTween.move(player, new Vector3(sitPosX, sitPosY, sitPosZ), tweenTime);
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
        LeanTween.move(player, new Vector3(unsitPosX, unsitPosY, unsitPosZ), tweenTime);
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
