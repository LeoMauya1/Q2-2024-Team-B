using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHeadLight : MonoBehaviour
{
    public Animator animator;
    public GameObject lightPositionFront;
    public GameObject lightPositionLeft;
    public GameObject lightPositionRight;
    public string clipName;

    public void FindAnimation()
    {
        if (clipName == "Gangler Front" || clipName == "Gangler Back" || clipName == "Gangler Watching")
        {
            lightPositionFront.SetActive(true);
            lightPositionLeft.SetActive(false);
            lightPositionRight.SetActive(false);
        }
        else if (clipName == "Gangler Left")
        {
            lightPositionLeft.SetActive(true);
            lightPositionFront.SetActive(false);
            lightPositionRight.SetActive(false);
        }
        else if (clipName == "Gangler Right")
        {
            lightPositionRight.SetActive(true);
            lightPositionFront.SetActive(false);
            lightPositionLeft.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FindAnimation();
    }
}


