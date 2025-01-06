using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InkSplot : MonoBehaviour
{
    public float inkTime;
    public GameObject ink;
    public bool inkOn;
    public Color inkColor;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
    }

    [ContextMenu("TurnInkOn")]

    public void TurnInkOn()
    {
        inkOn = true;
        audioSource.Play();
        ink.gameObject.GetComponent<Image>().color = inkColor;
    }
    void Update()
    {
        if (inkOn == true)
        {
            inkColor.a -= inkTime * Time.deltaTime;
            ink.gameObject.GetComponent<Image>().color = inkColor;
        }
        if (inkColor.a <= 0)
        {
            inkOn = false;
            inkColor.a = 1;
        }
    }
}
