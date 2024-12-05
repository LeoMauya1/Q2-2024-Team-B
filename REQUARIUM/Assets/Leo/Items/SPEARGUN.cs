using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class SPEARGUN : MonoBehaviour
{
    [Header("INPUTS")]
    public InputAction launchSpear;
    public Transform playerPos;

    [Header("SHOOTING")]
    public GameObject spears;
    public GameObject FiringPoint;


    [Header("STUN TIME")]
    public float stunTime;
    [Header("SPEAR ATRIBUTES")]
    public Rigidbody rb;
    public Vector3 spearPosition;
    public Vector3 spearRotation;


    private PLAYERCONTROLLER shootingEvent;
    private bool hasShot;

  
    



    private void Update()
    {
        transform.position = playerPos.position + playerPos.TransformDirection(spearPosition);
        transform.rotation = playerPos.rotation * Quaternion.Euler(spearRotation);


        if(hasShot)
        {
            spears.transform.position = new Vector3(transform.position.x + 4, transform.position.y, transform.position.z);
            hasShot =! hasShot;
        }

    }


    private void Awake()
    {
        shootingEvent = new PLAYERCONTROLLER();
    }


    private void OnEnable()
    {
        launchSpear = shootingEvent.GamePlay1.shootSpear;
        launchSpear.Enable();
        launchSpear.performed += ShootSpear;
    }
    private void OnDisable()
    {
        launchSpear.Disable();
    }

    private void ShootSpear(InputAction.CallbackContext contxt)
    {
        Debug.Log("spear was shot!");
        Instantiate(spears, FiringPoint.transform.position, FiringPoint.transform.rotation);
        hasShot = true;
        
    }
}
