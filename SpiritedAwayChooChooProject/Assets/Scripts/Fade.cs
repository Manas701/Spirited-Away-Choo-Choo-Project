using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public float fadeTime;

    // Start is called before the first frame update
    void Start()
    {
        Color tmp = GetComponent<MeshRenderer>().material.color;
        tmp.a = 0f;
        GetComponent<MeshRenderer>().material.color = tmp;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void FadeBlack()
    {
        LeanTween.alpha(gameObject, 1f, fadeTime);

    }

    public void UnfadeBlack()
    {
        LeanTween.alpha(gameObject, 0f, fadeTime);
    }
}
