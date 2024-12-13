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







    public List<GameObject> Inventory = new List<GameObject>();
    public PLAYERCONTROLLER playerInputActions;
    public InputAction ScrollingEvent;
    private int currentIndex = 0;
    private int scrollValue;
    private int previousIndex;
    private Transform Item;









    private void Start()
    {
        
    }



    private void Update()
    {
       
        //Inventory[currentIndex].transform.position = playerPos.position + playerPos.TransformDirection(itemPos);
        //Inventory[currentIndex].transform.rotation = playerPos.rotation * Quaternion.Euler(rotationOffset);
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

        Inventory[previousIndex].SetActive(false);
     
        foreach (var item in Inventory)
        
            Inventory[currentIndex].SetActive(true);
    }
    



        

       

       
       
    






}



