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
        
        


          if(battery > 0 && batteryGageOpen)
          {
             batteryTime = battery * 60 + batteryTime;
            batteryGageOpen = false;
             
          }


        if ( batteryDead)
        {
        
            switchOn.enabled = false;
            StopAllCoroutines();

           
            
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
            
        }
      

    }


    private void FlashLightActions(InputAction.CallbackContext contxt)
    {
        


        if(!batteryDead || battery > 0 )
        {
            Debug.Log("running corutine"); 
            batteryDead = false;
            StartCoroutine(FlashLightCooldown());
        }

        





    }

    private IEnumerator FlashLightCooldown()
    {

        flashLightHitbox.enabled = !flashLightHitbox.enabled;
        Debug.Log("light on");
        switchOn.enabled = !switchOn.enabled;

      
        

        while (batteryTime > 0)
        {
          
            batteryTime -= Time.deltaTime;
            batteryTime = Mathf.Max(batteryTime, 0);
            MinutesPassed += Time.deltaTime;
            //Debug.Log(batteryTime);
            yield return null;
           
        
           
        }

        batteryDead = true;
        batteryGageOpen = true;
        Debug.Log("dead");
        Debug.Log(battery);
        

        
    }


    


}
