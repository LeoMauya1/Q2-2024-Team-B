using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UIElements;


public class PLAYER_INVENTORY : MonoBehaviour
{
   public List<GameObject> Inventory = new List<GameObject>();
    public PLAYERCONTROLLER playerInputActions;
    public InputAction ScrollingEvent;
    private int currentIndex = 0;








    private void Start()
    {
        
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

        currentIndex++;
       currentIndex = (int)Mathf.Repeat(currentIndex,Inventory.Count);
        Debug.Log(currentIndex);

    }

    private void ToItem(int currentIndex)
    {
       foreach (var item in Inventory)
        {
            
        }
    }






}



