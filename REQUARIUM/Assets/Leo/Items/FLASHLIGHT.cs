using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using System;
using TMPro;

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
    public int battery;
    private bool batteryDead = false;
    private float MinutesPassed;
    private bool batteryGageOpen = true;
    private Image batterySlider;
    public float slideSpeed;
    private AudioSource audioSource;
    public AudioClip[] flashLightSounds;
    public Animator animator;
    public float sliderEnd;
    private int soundOrder = 0;
    private float sliderTime;
    private AudioClip soundOn;
    private float maxBatterySlider;
    private GameObject flashTut;
    private GameObject critHealthText;

    public Color baseColor = Color.white;
    public Color possessedColor;

    public Sprite regularSprite;
    public Sprite possessedSprite;
    public SpriteRenderer hand;
    void Start()
    {
        battery = SaveDataManager.Instance.daveSata.batteries;
        switchOn.transform.SetParent(null);
        audioSource = GetComponent<AudioSource>();
        

    }

    private void Awake()

    {
        
        playerInputActions = new PLAYERCONTROLLER();
        flashTut = GameObject.Find("FLASHLIGHT TEXT");
        critHealthText = GameObject.Find("CRITICAL");
        batterySlider = GameObject.Find("Battery Slider").GetComponent<Image>();

    }

    public void OnEnable()
    {
        
        flashLightButton = playerInputActions.GamePlay1.FlashLight;
        flashLightButton.Enable();
        flashTut.gameObject.SetActive(true);

        flashLightButton.performed += FlashLightActions;
        



    }
    public void OnDisable()
    {
        flashLightButton.Disable();
        flashLightButton.performed -= FlashLightActions;
        flashTut.gameObject.SetActive(false);
        critHealthText.gameObject.SetActive(false);


    }


    // Update is called once per frame
    void Update()
    {
        if (isPossessed == true)
        {
            switchOn.color = possessedColor;
            hand.sprite = possessedSprite;
        }
        else
        {
            switchOn.color = baseColor;
            hand.sprite = regularSprite;
        }
            
        

        switchOn.transform.position = Camera.main.transform.position + Camera.main.transform.TransformDirection(lightPos);
        switchOn.transform.rotation = Camera.main.transform.rotation * Quaternion.Euler(rotationOffset);
        
        


          if(battery > 0 && batteryGageOpen)
          {
             batteryTime = battery * 60 + batteryTime;

            maxBatterySlider = batteryTime;
            batterySlider.fillAmount = 1;
            batteryGageOpen = false;
            critHealthText.SetActive(false);
            animator.SetBool("flashIdle", true);
            animator.SetBool("fewSeconds", false);
            animator.SetBool("batteryDown", false);

        }


        if ( batteryDead)
        {
        
            switchOn.enabled = false;
            
            StopAllCoroutines();

           
            
        }
       
        if(switchOn.enabled == false && !batteryDead)
        {
            
            StopAllCoroutines();
        
           
        }

        if (MinutesPassed > 60f && switchOn.enabled == true)
        {
            Debug.Log("one battery donw");
            battery -= 1;
            SaveDataManager.Instance.daveSata.batteries -= 1;
            MinutesPassed = 0;
            StartCoroutine(FlashLightFlicker());
            
        }
        if( batteryTime <= 25f)
        {
            critHealthText.SetActive(true);
            Debug.Log("running out of time");
            animator.SetBool("flashIdle",false);
            animator.SetBool("fewSeconds", true);
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
        soundOrder = (int)Mathf.Repeat(soundOrder + 1, 2);
      
        

        while (batteryTime > 0)
        {
            
            
            batteryTime -= Time.deltaTime;
            batteryTime = Mathf.Max(batteryTime, 0);
            MinutesPassed += Time.deltaTime;
            batterySlider.fillAmount = batteryTime / maxBatterySlider;
            //Debug.Log(batteryTime);
            yield return null;
           
        
           
        }
     
   
        batteryDead = true;
        batteryGageOpen = true;
        audioSource.PlayOneShot(flashLightSounds[2]);
        Debug.Log("dead");
        Debug.Log(battery);
        

        
    }


    private IEnumerator FlashLightFlicker()
    {
       
        animator.SetBool("flashIdle", false);
        animator.SetBool("batteryDown", true);
        yield return new WaitForSeconds(0.7f);
        animator.SetBool("flashIdle", true);
        animator.SetBool("batteryDown", false);

    }
    


    private void FlashlightButtonSound(InputAction.CallbackContext contxt)
    {
        
    }


}
