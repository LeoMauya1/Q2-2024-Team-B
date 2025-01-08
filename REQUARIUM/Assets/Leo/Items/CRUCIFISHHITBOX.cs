using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class CRUCIFISHHITBOX : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Artifact"))
        {

            if (CRUCIFISH.isHaunted == true)
            {
                other.GetComponent<Artifact>().price = other.GetComponent<Artifact>().price / 2;
            }
            if (other.GetComponent<Artifact>().isHaunted == false)
            {
                Destroy(other.gameObject);
            }
            else if (other.GetComponent<Artifact>().isHaunted == true)
            {
                other.GetComponent<Artifact>().particle.Play();
                other.GetComponent<Artifact>().isHaunted = false;
            }
            if (other.GetComponent<Artifact>().isSound == true)
            {
                other.GetComponent<Artifact>().audioSource.Stop();
            }
        }
    }


    
}

