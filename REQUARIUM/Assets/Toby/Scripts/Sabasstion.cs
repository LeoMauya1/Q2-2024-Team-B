using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sabasstion : MonoBehaviour
{
    public List<AudioClip> bassClipsIndex;
    public List<AudioClip> currentClips;
    public AudioSource bassSource;
    public Animator bassAnimator;
    public void PlaySong()
    {
        bassSource.Stop();
        bassSource.clip = currentClips[0];
        currentClips.Remove(currentClips[0]);
        if (currentClips.Count <= 0)
        {
            currentClips = bassClipsIndex;
        }
        bassSource.Play();
    }
    void Start()
    {
        currentClips = bassClipsIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (bassSource.isPlaying == true)
        {
            bassAnimator.SetBool("Singing", true);
        }
        else if (bassSource.isPlaying == false)
        {
            bassAnimator.SetBool("Singing", false);
        }
    }
}
