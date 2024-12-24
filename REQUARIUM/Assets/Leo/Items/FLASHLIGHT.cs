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
    private float batteryTime = 0;
    public int battery = 1;
    private bool batteryDead = false;
    private float MinutesPassed;
    void Start()
    {
        switchOn.transform.SetParent(null);
        //battery = SaveDataManager.Instance.daveSata.batteries; null rn
       
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
        MinutesPassed += Time.deltaTime;

        if ( batteryDead)
        {
            StopCoroutine(FlashLightCooldown());
            switchOn.enabled = false;
            

        }
        if (MinutesPassed == 60f)
        {
            //Debug.Log("one battery donw");
            battery -= 1;
            //Debug.Log(battery);
        }




    }


    private void FlashLightActions(InputAction.CallbackContext contxt)
    {
        if(!batteryDead || battery > 0 )
        {
            StartCoroutine(FlashLightCooldown());
        }
        
      
        
       

    }

    private IEnumerator FlashLightCooldown()
    {

        flashLightHitbox.enabled = !flashLightHitbox.enabled;
        //Debug.Log("light on");
        switchOn.enabled = !switchOn.enabled;

        batteryTime = battery * 60 + batteryTime;
        //Debug.Log(batteryTime/60);
        //Debug.Log("THATS YOUR BATTERY TIME!");
        

        while (batteryTime > 0)
        {
            //Debug.Log("Battery depleating");
            batteryTime -= Time.deltaTime;
            Mathf.Clamp(batteryTime,0, batteryTime);
            //Debug.Log(batteryTime);
            yield return null;
           
           
        }
        
        batteryDead = true;
        //Debug.Log("dead");
        //Debug.Log(battery);
        

        
    }


    


}
