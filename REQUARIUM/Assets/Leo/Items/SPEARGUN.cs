using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows.Speech;

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
    public Transform playerPos;
    public Vector3 spearPos;
    public Vector3 spearRotation;

    private PLAYERCONTROLLER shootingEvent;
    private bool hasShot;
    private GameObject spear;



    private void Update()
    {
        transform.position = playerPos.position + playerPos.TransformDirection(spearPos);
        transform.rotation = playerPos.rotation * Quaternion.Euler(spearRotation);





        if (hasShot)
        {
            Debug.Log("POW!");

            spear.transform.position = new Vector3(spear.transform.position.x, spear.transform.position.y, spear.transform.position.z * 20 * Time.deltaTime);
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

        spear = Instantiate(spears, FiringPoint.transform.position, FiringPoint.transform.rotation);
        hasShot = true;

        
    }
}
