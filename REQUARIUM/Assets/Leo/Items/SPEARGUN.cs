using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class SPEARGUN : MonoBehaviour
{
    [Header("INPUTS")]
    public InputAction launchSpear;
    
    [Header("SHOOTING")]
    public GameObject spears;
    public GameObject FiringPoint;


    [Header("SPEAR UI")]
    public Sprite itemReticle;
    
  
    [Header("SPEAR ATRIBUTES")]
    public Rigidbody rb;
    public Transform playerPos;
    public Vector3 spearPos;
    public Vector3 spearRotation;
    public float spearSpeed;
    public float stunTime;

    private PLAYERCONTROLLER shootingEvent;
    private bool hasShot;
    private GameObject spear;
    public Camera fp;
    private Vector3 targetpoint;

    



    private void Update()
    {
       





        if (hasShot && spear != null)
        {
            Debug.Log("POW!");

           spear.transform.position = Vector3.MoveTowards(spear.transform.position, targetpoint, spearSpeed * Time.deltaTime);
            
        }
            
     
    }
    private void Start()
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
        launchSpear.performed -= ShootSpear;
       
    }

    private void ShootSpear(InputAction.CallbackContext contxt)
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
       

        if ( Physics.Raycast(ray,out hit))
        {
            targetpoint = hit.point;
            Debug.Log("hit item");
        }
        else
        {
            targetpoint = ray.GetPoint(75);
        }
        Vector3 direction = targetpoint - FiringPoint.transform.position;

        Debug.Log("spear was shot!");

        spear = Instantiate(spears, FiringPoint.transform.position, FiringPoint.transform.rotation);
        
        hasShot = true;

        

        
    }

    
}
