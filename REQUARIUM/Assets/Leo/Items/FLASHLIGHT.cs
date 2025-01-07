using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine.UI;
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
    private Slider batterySlider;
    public float slideSpeed;
    private AudioSource audioSource;
    public AudioClip[] flashLightSounds;
    private int soundOrder = 0;

    private AudioClip soundOn;
    
    void Start()
    {
        switchOn.transform.SetParent(null);
        audioSource = GetComponent<AudioSource>();
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
        batterySlider = GameObject.Find("Battery Slider").GetComponent<Slider>();

        switchOn.transform.position = Camera.main.transform.position + Camera.main.transform.TransformDirection(lightPos);
        switchOn.transform.rotation = Camera.main.transform.rotation * Quaternion.Euler(rotationOffset);
        
        


          if(battery > 0 && batteryGageOpen)
          {
             batteryTime = battery * 60 + batteryTime;
            
            batterySlider.maxValue = batteryTime;
            batterySlider.value = 0;
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
        audioSource.PlayOneShot(flashLightSounds[soundOrder]);
        soundOrder = (int)Mathf.Repeat(soundOrder + 1, flashLightSounds.Length);
      
        

        while (batteryTime > 0)
        {
          
            batteryTime -= Time.deltaTime;
            batteryTime = Mathf.Max(batteryTime, 0);
            MinutesPassed += Time.deltaTime;
            batterySlider.value = Mathf.MoveTowards(batterySlider.maxValue, batterySlider.minValue, batteryTime);
            //Debug.Log(batteryTime);
            yield return null;
           
        
           
        }

        batteryDead = true;
        batteryGageOpen = true;
        audioSource.PlayOneShot(flashLightSounds[2]);
        Debug.Log("dead");
        Debug.Log(battery);
        

        
    }


    private void FlashlightButtonSound(InputAction.CallbackContext contxt)
    {
        
    }


}
