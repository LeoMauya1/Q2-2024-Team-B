using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class SPEARGUN : MonoBehaviour
{
    [Header("INPUTS")]
    public InputAction launchSpear;
    
    [Header("SHOOTING")]
    public GameObject spears;
    public GameObject FiringPoint;


    [Header("STUN TIME")]
    public float stunTime;
    [Header("SPEAR ATRIBUTES")]
    public Rigidbody rb;

    private PLAYERCONTROLLER shootingEvent;



    private void Update()
    {
        
       
     
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
        rb.AddForce(Vector3.forward * 5, ForceMode.Impulse);
    }
}
