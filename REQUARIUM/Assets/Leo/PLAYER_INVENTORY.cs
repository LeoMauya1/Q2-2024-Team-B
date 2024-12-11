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
    private int currentIndex;








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
        ScrollingEvent.performed += ScrollItems;
    }
    private void OnDisable()
    {
        ScrollingEvent.Disable();
    }




    private void ScrollItems(InputAction.CallbackContext context)
    {
        currentIndex = Inventory.IndexOf(gameObject);
        Debug.Log(Inventory.Count);
        Debug.Log(currentIndex);
        Mathf.Repeat(currentIndex, Inventory.LastIndexOf(gameObject));

    }

}



