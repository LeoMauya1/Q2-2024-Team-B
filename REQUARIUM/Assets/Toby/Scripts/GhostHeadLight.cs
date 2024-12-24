using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHeadLight : MonoBehaviour
{
    public Animator animator;
    public GameObject lightPositionFront;
    public GameObject lightPositionLeft;
    public GameObject lightPositionRight;

    public void FindAnimation (Animator animator)
    {
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == "Gangler Front" || clip.name == "Gangler Back" || clip.name == "Gangler Watching")
            {
                lightPositionFront.SetActive(true);
                lightPositionLeft.SetActive(false);
                lightPositionRight.SetActive(false);
            }
            else if (clip.name == "Gangler Left")
            {
                lightPositionLeft.SetActive(true);
                lightPositionFront.SetActive(false);
                lightPositionRight.SetActive(false);
            }
            else if (clip.name == "Gangler Right")
            {
                lightPositionRight.SetActive(true);
                lightPositionFront.SetActive(false);
                lightPositionLeft.SetActive(false);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FindAnimation(animator);
    }
}
