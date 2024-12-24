using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class JumpscareController : MonoBehaviour
{
    public Jumpscare jumpscare;
    public GameObject parent;
    public bool jumping;
    public GameObject spawnedJumpscare;
    public GameObject player;
    public float storedSpeed;
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        parent = GameObject.FindGameObjectWithTag("Canvas");
    }

    public void SpawnScare()
    {
        storedSpeed = player.GetComponent<PlayerMovement>().walkSpeed;
        player.GetComponent<PlayerMovement>().walkSpeed = 0;
        jumping = true;
        spawnedJumpscare = Instantiate(jumpscare.jumpscareImage, parent.transform);
        jumpscare.jumpscareImage.transform.parent = parent.transform;
    }
    public void UnJumpscare()
    {
        player.GetComponent<PlayerMovement>().walkSpeed = storedSpeed;
        jumpscare.jumpscareTime = jumpscare.jumpscareMax;
        jumping = false;
        Destroy(spawnedJumpscare);
    }

    public void OnTriggerEnter(Collider other)
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (jumping == true)
        {
            jumpscare.jumpscareTime -= Time.deltaTime;
        }
        if (jumpscare.jumpscareTime <= 0)
        {
            UnJumpscare();
        }
    }
}
