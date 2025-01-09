using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomNoise : MonoBehaviour
{

    private float timePassed;
    public AudioClip[] ambientNoises;
    private AudioSource audioSource;
    private int randomInt;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;





        if(timePassed >= 30f)
        {
            randomInt = Random.Range(0, ambientNoises.Length);
            audioSource.PlayOneShot(ambientNoises[randomInt]);
            timePassed = 0;
        }
    }
}
