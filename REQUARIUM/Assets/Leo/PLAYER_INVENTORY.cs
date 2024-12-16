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







    public List<GameObject> Inventory = new List<GameObject>();
    public PLAYERCONTROLLER playerInputActions;
    public InputAction ScrollingEvent;
    private int currentIndex = 0;
    private int scrollValue;
    private int previousIndex;
    private Transform Item;
    private List<GameObject> instantiatedItems = new List<GameObject>();
    private GameObject instantiatedItem;









    private void Start()
    {
        InstantiateItems();
    }



    private void Update()
    {
       if(instantiatedItems.Count > 0)
        {

            instantiatedItems[currentIndex].transform.position = Camera.main.transform.position + Camera.main.transform.TransformDirection(itemPos);
            instantiatedItems[currentIndex].transform.rotation = Camera.main.transform.rotation * Quaternion.Euler(rotationOffset);
        }
        if(instantiatedItems[currentIndex].tag == "FlashLight")
        {
            instantiatedItems[currentIndex].transform.position = Camera.main.transform.position + Camera.main.transform.TransformDirection(itemPos);
            instantiatedItems[currentIndex].transform.rotation = Camera.main.transform.rotation * Quaternion.Euler(flashLightsRotationOffset);
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
        if(scrollValue < 0)
        {
            currentIndex = (int)Mathf.Repeat(currentIndex - 1, Inventory.Count);
            Debug.Log("left in the inventory");
            Debug.Log(currentIndex);
            ToItem(currentIndex, previousIndex);

        }
        if(scrollValue > 0)
        {
            currentIndex = (int)Mathf.Repeat(currentIndex + 1, Inventory.Count);
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
    



        

       

       
       
    






}



