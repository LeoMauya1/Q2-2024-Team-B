using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class CRUCIFISHHITBOX : MonoBehaviour
{
    public GameObject artifactHit;
    public bool artifactIsHit;
    public float timeToDestroy;
    public float timeToDestroyMax;
    public BoxCollider hitbox;
    public void DestroyArtifact()
    {
        artifactIsHit = false;
        timeToDestroy = timeToDestroyMax;
        Destroy(artifactHit);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Artifact"))
        {
            artifactHit = other.gameObject;
            if (other.GetComponent<Artifact>().isSound == true)
            {
                other.GetComponent<Artifact>().audioSource.Stop();
            }
            if (CRUCIFISH.isHaunted == true)
            {
                other.GetComponent<Artifact>().price = other.GetComponent<Artifact>().price / 2;
            }
            if (other.GetComponent<Artifact>().isHaunted == false)
            {
                other.GetComponent<SpriteRenderer>().enabled = false;
                other.GetComponent<Artifact>().dirt.Play();
                other.GetComponent<Artifact>().dirtSound.Play();
                artifactIsHit = true;
            }
            else if (other.GetComponent<Artifact>().isHaunted == true)
            {
                other.GetComponent<Artifact>().particle.Play();
                other.GetComponent<Artifact>().particleSound.Play();
                other.GetComponent<Artifact>().isHaunted = false;
                other.GetComponent<Artifact>().lightIntensity = 0;
                other.GetComponent<Artifact>().audioSource.Stop();
            }
        }
    }
    public void Update()
    {
        if (artifactIsHit == true)
        {
            timeToDestroy -= Time.deltaTime;
            if (timeToDestroy <= 0)
            {
                if (artifactHit.GetComponent<Artifact>().isTutorial == true)
                {
                    artifactHit.GetComponent<Artifact>().infoText.SetActive(false);
                }
                DestroyArtifact();
            }
        }
    }



}

