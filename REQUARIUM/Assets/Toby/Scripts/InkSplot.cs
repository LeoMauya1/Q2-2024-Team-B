using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkSplot : MonoBehaviour
{
    public float inkTime;
    public float inkTimeMax;
    public GameObject ink;
    // Start is called before the first frame update
    void Start()
    {
        inkTime = inkTimeMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (ink.activeSelf == true)
        {
            inkTime -= Time.deltaTime;
        }
        if (inkTime <= 0)
        {
            ink.SetActive(false);
            inkTime = inkTimeMax;
        }
    }
}
