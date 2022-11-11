using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{   
    // A measure of magnitude for the shake. Tweak based on your preference
    public float shakeMagnitude;
    
    // The initial position of the GameObject
    Vector3 initialPosition;

    // The timer in between shakes
    private float shakeTimer = 0f;

    // The time in between shakes
    private float shakeTime;
    public float shakeTimeMin;
    public float shakeTimeMax;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.localPosition;     
    }

    // Update is called once per frame
    void Update()
    {
        shakeTimer += Time.deltaTime;
        shakeTime = Random.Range(shakeTimeMin, shakeTimeMax);
        if (shakeTimer >= shakeTime)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
        }
    }
}
