using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMovement : MonoBehaviour
{
    public float Speed = 2.0f;
    public float Xspawnpoint = 0.0f;
    public float Xendpoint = 25.0f;
    Vector3 begin;

    // Start is called before the first frame update
    void Start()
    {
        begin = new Vector3(Xspawnpoint, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == new Vector3(Xendpoint, transform.position.y, transform.position.z)){
            transform.position = begin;
        }
        else{
            transform.position =  Vector3.MoveTowards(transform.position, new Vector3(Xendpoint, transform.position.y, transform.position.z), Speed * Time.deltaTime);
        }
        
    }
}
