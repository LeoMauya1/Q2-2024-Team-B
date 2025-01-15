using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bubbles : MonoBehaviour
{
    public UnityEvent bubbleEvent;

    public void EventEnable()
    {
        bubbleEvent.Invoke();
    }
    void Start()
    {
        EventEnable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
