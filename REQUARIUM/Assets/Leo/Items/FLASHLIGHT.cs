using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class FLASHLIGHT : MonoBehaviour
{
    public InputAction flashLightButton;
    public Light switchOn;
    public Vector3 lightPos;
    public Quaternion lightRot;
    public Transform playerPos;
    public Transform camPos;
    public Vector3 rotationOffset;
    private PLAYERCONTROLLER playerInputActions;
    

    void Start()
    {

    }

    private void Awake()
    {
        playerInputActions = new PLAYERCONTROLLER();
    }

    public void OnEnable()
    {
        flashLightButton = playerInputActions.GamePlay1.FlashLight;
        flashLightButton.Enable();
        flashLightButton.performed += FlashLightActions;
        



    }
    public void OnDisable()
    {
        flashLightButton.Disable();
        flashLightButton.performed -= FlashLightActions;
    }


    // Update is called once per frame
    void Update()
    {

      
        transform.position = playerPos.position + playerPos.TransformDirection(lightPos);
        transform.rotation = playerPos.rotation * Quaternion.Euler(rotationOffset);

        
    }


    private void FlashLightActions(InputAction.CallbackContext contxt)
    {
        
        
            Debug.Log("light on");
            switchOn.enabled =! switchOn.enabled;
        
       

    }
    
}
