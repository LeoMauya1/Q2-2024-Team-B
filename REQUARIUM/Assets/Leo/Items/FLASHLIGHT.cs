using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FLASHLIGHT : MonoBehaviour
{
    public InputActionReference flashLightButton;
    public Light switchOn;
    private bool wasOn;
    public Transform playerPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        flashLightActions();
    }

    private void FixedUpdate()
    {

        
    }

    private void flashLightActions()
    {
        wasOn = switchOn.enabled;

        if (flashLightButton.action.WasPerformedThisFrame())
        {
            Debug.Log("light on");
            switchOn.enabled = true;
        }
        if (flashLightButton.action.WasPressedThisFrame() && wasOn)
        {
            Debug.Log("light off");
            switchOn.enabled = false;

        }

    }
}
