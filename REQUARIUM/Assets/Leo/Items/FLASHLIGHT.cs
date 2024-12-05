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
    public Vector3 flashLightPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       transform.position = playerPos.position + flashLightPos;
        flashLightActions();
    }

    private void FixedUpdate()
    {

        
    }

    private void flashLightActions()
    {
       

        if (flashLightButton.action.WasPerformedThisFrame())
        {
            Debug.Log("light on");
            switchOn.enabled =! switchOn.enabled;
        }
        

    }
}
