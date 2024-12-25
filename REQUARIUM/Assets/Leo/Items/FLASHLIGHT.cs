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
    public int battery = 3;
    private bool batteryDead = false;
    private float MinutesPassed;
   
  


    private bool wasON = true;
    private bool batteryGageOpen = true;
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


        if (battery > 0 && batteryGageOpen)
        {
            batteryTime = battery * 60 + batteryTime;
            batteryGageOpen = false;
            batteryDead = false;

        }

        if(battery > 0 && !batteryDead)
        {
            batteryDead = false;
        }


        if ( batteryDead)
        {
            StopAllCoroutines();
            switchOn.enabled = false;
           

           
         
        }
       
        if(switchOn.enabled == false && !batteryDead)
        {
            Debug.Log("flash light cooldown off");
            StopAllCoroutines();
            Debug.Log(batteryTime);
        }

        if (MinutesPassed > 60f && switchOn.enabled == true)
        {
            Debug.Log("one battery donw");
            battery -= 1;
            MinutesPassed = 0;
            return;
        }




    }


    private void FlashLightActions(InputAction.CallbackContext contxt)
    {
        wasON = !wasON;

        Debug.Log("flashlight clicked");

        if(!batteryDead || battery > 0 )
        {
            Debug.Log("running corutine"); 
            StartCoroutine(FlashLightCooldown());
        }

        





    }

    private IEnumerator FlashLightCooldown()
    {

        flashLightHitbox.enabled = !flashLightHitbox.enabled;
        switchOn.enabled = !switchOn.enabled;
        Debug.Log("light on");

      
        

        while (batteryTime > 0)
        {
            //Debug.Log("Battery depleating");
            batteryTime -= Time.deltaTime;
            batteryTime = Mathf.Max(batteryTime, 0);
            MinutesPassed += Time.deltaTime;
        
            yield return null;
           
        
           
        }

        batteryDead = true;
        batteryGageOpen = true;
        Debug.Log("dead");
        Debug.Log(battery);
        

        
    }


    


}
