using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventOnCollision : MonoBehaviour
{
    public UnityEvent eventOnTriggerEnter;
    
    public UnityEvent eventOnTriggerExit;
    
    public UnityEvent eventOnCollisionEnter;

    public UnityEvent eventOnCollisionExit;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            eventOnTriggerEnter.Invoke();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            eventOnTriggerExit.Invoke();
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            eventOnCollisionEnter.Invoke();
        }
    }

    public void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            eventOnCollisionExit.Invoke();
        }
    }
}

