using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Artifact : MonoBehaviour
{
    public GameObject thisArtifact;
    public Sprite artifactLook;
    public List<Sprite> artifactNormalSprites;
    public List<Sprite> artifactHauntedSprites;
    public AudioClip artifactSound;
    public List<AudioClip> artifactSounds;
    public Light lightCue;
    public List<Light> lightCues;

    public bool HasOctopus;
    public int price;
    public int corruptionCount;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
