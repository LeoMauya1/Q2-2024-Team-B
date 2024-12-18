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
    public static bool isPossessed;
    public Collider flashLightHitbox;
    private float batteryTime = 360f;
    private int battery;
    private bool batteryDead;
    void Start()
    {
        switchOn.transform.SetParent(null);
        battery = SaveDataManager.Instance.daveSata.batteries;
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
        switchOn.transform.position = Camera.main.transform.position + Camera.main.transform.TransformDirection(lightPos);
        switchOn.transform.rotation = Camera.main.transform.rotation * Quaternion.Euler(rotationOffset);

        
    }


    private void FlashLightActions(InputAction.CallbackContext contxt)
    {

        if( !batteryDead )
        {
        flashLightHitbox.enabled =! flashLightHitbox.enabled;
            Debug.Log("light on");
            switchOn.enabled = !switchOn.enabled;
        }

        StartCoroutine(FlashLightCooldown(battery, batteryTime));
        
       

    }

    private IEnumerator FlashLightCooldown(int battery, float batteryTime)
    {


        battery = battery * 60;
        batteryTime = battery + batteryTime;
        Debug.Log(batteryTime/60);
        Debug.Log("THATS YOUR BATTERY TIME!");
        yield return new WaitForSeconds(batteryTime);
        batteryDead = true;
    }


    


}
