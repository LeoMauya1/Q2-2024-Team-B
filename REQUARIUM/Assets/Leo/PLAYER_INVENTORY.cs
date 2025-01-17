using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UIElements;


public class PLAYER_INVENTORY : MonoBehaviour
{

    [Header("Item placement")]
    public Transform playerPos;
    public Vector3 itemPos;
    public Vector3 rotationOffset;
    [Header("FlashLight rotation")]
    public Vector3 flashLightsRotationOffset;
    
    [Header("spearGun Placement")]
    public Vector3 spearGunPos;
    public Vector3 spearRot;

    [Header("Crucifish Placement")]
    public Vector3 crucifishPos;
    public Vector3 crucirotation;





    public List<GameObject> Inventory = new List<GameObject>();
    public PLAYERCONTROLLER playerInputActions;
    public InputAction ScrollingEvent;
    private int currentIndex = 0;
    private int scrollValue;
    private int previousIndex;
    private Transform Item;
    private List<GameObject> instantiatedItems = new List<GameObject>();
    private GameObject instantiatedItem;

    public Camera rightHandCam;
    private Light flashLight;
    private GameObject flashLightUI;
    public AudioClip[] scrollSounds;
    private AudioSource audioSource;
    private int soundOrder = 0;








    private void Start()
    {
        InstantiateItems();
        audioSource = GetComponent<AudioSource>();
    }



    private void Update()
    {

        instantiatedItems[currentIndex].transform.SetParent(GameObject.Find("Main Camera").GetComponent<Camera>().transform);



        if(instantiatedItems[currentIndex].tag == "FlashLight")
        {
            instantiatedItems[currentIndex].transform.position = rightHandCam.transform.position + rightHandCam.transform.TransformDirection(itemPos);
            instantiatedItems[currentIndex].transform.rotation = rightHandCam.transform.rotation * Quaternion.Euler(flashLightsRotationOffset);
        }
        if (instantiatedItems[currentIndex].tag == "test Variant")
        {
            instantiatedItems[currentIndex].transform.position = rightHandCam.transform.position + rightHandCam.transform.TransformDirection(spearGunPos);
            instantiatedItems[currentIndex].transform.rotation = rightHandCam.transform.rotation * Quaternion.Euler(spearRot);

        }
        if (instantiatedItems[currentIndex].tag == "Crucifish")
        {
            
            instantiatedItems[currentIndex].transform.position = rightHandCam.transform.position + rightHandCam.transform.TransformDirection(crucifishPos);
            instantiatedItems[currentIndex].transform.rotation = rightHandCam.transform.rotation * Quaternion.Euler(crucirotation);
            Debug.Log("crucu");
        }
    }

    private void Awake()
    {
        playerInputActions = new PLAYERCONTROLLER();
    }

    private void OnEnable()
    {
        ScrollingEvent = playerInputActions.GamePlay1.scrolling;
        ScrollingEvent.Enable();
        ScrollingEvent.performed += Scroll;
    }
    private void OnDisable()
    {
        ScrollingEvent.Disable();
        ScrollingEvent.performed -= Scroll;
    }




    private void Scroll(InputAction.CallbackContext context)
    {

     
        scrollValue = MathF.Sign(ScrollingEvent.ReadValue<float>());
        previousIndex = currentIndex;
       
        if (scrollValue < 0)
        {
            currentIndex = (int)Mathf.Repeat(currentIndex - 1, Inventory.Count);
            audioSource.PlayOneShot(scrollSounds[soundOrder]);
            soundOrder = (int)Mathf.Repeat(soundOrder - 1, scrollSounds.Length);
            Debug.Log("left in the inventory");
            Debug.Log(currentIndex);
            ToItem(currentIndex, previousIndex);

        }
        if(scrollValue > 0)
        {
            currentIndex = (int)Mathf.Repeat(currentIndex + 1, Inventory.Count);
            audioSource.PlayOneShot(scrollSounds[soundOrder]);
            soundOrder = (int)Mathf.Repeat(soundOrder + 1, scrollSounds.Length);
            Debug.Log("right in the inventory");
            Debug.Log(currentIndex);
            ToItem(currentIndex, previousIndex);

        }
      





    }

    private void ToItem(int currentIndex, int previousIndex)
    {


        
        instantiatedItems[previousIndex].SetActive(false);
        


        foreach (var item in Inventory)
        {

            instantiatedItems[currentIndex].SetActive(true);
           
            
            if (instantiatedItems[previousIndex].tag == "FlashLight")
            {
                flashLight = GameObject.Find("light").GetComponent<Light>();
                flashLightUI = GameObject.Find("Battery Slider");
                //flashLightUI.GetComponent<Animator>().SetBool("batteryIsSelected", false);
                flashLight.GetComponentInChildren<SphereCollider>().enabled = false;
                flashLight.enabled = false;
                Debug.Log("flash turned off");
                
            }
            if (instantiatedItems[currentIndex].tag == "FlashLight")
            {
                flashLightUI = GameObject.Find("Battery Slider");
                //flashLightUI.GetComponent<Animator>().SetBool("batteryIsSelected", true);
}

        }

      
    }

    private void InstantiateItems()
    {
        foreach(var item in Inventory)
        {
            if(item != null)
            {
                instantiatedItem = Instantiate(item, playerPos.position, playerPos.rotation);
                instantiatedItem.SetActive(false);
                instantiatedItems.Add(instantiatedItem);

            }
        }

        if(instantiatedItems.Count > 0)
        {
            instantiatedItems[0].SetActive(true);
        }
    }

    private void playItemJingle()
    {
       
    }
    



        

       

       
       
    






}



