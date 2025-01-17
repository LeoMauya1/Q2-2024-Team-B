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
    public Sprite shootingSprite; 


    private PLAYERCONTROLLER shootingEvent;
    private bool hasShot;
    private GameObject spear;
    public Camera fp;
    private Vector3 targetpoint;
    private Camera rightHandCam;
    public LayerMask spearLayer;
    private AudioSource audioSource;
    public AudioClip spearSound;
    private GameObject crucifishTut;
    private int spearAmount = 3;
    private SpriteRenderer spriteRenderer;
    private Sprite idleSpearGun;
    public static bool isHaunted;
    private Animator animator;

    private void Update()
    {




        FiringPoint.transform.position = Camera.main.transform.position + Camera.main.transform.TransformDirection(spearPos);
        FiringPoint.transform.rotation = Camera.main.transform.rotation * Quaternion.Euler(spearRotation);



        
            
     
    }
    private void Start()
    {

        //spearAmount = SaveDataManager.Instance.daveSata.spears;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        FiringPoint.transform.SetParent(null);
        


    }


    private void Awake()
    {
        shootingEvent = new PLAYERCONTROLLER();
        crucifishTut = GameObject.Find("SPEAR GUN");
    }


    private void OnEnable()
    {
        launchSpear = shootingEvent.GamePlay1.shootSpear;
        launchSpear.Enable();
        launchSpear.performed += ShootSpear;
        crucifishTut.SetActive(true);



    }
    private void OnDisable()
    {
        launchSpear.Disable();
        launchSpear.performed -= ShootSpear;
        crucifishTut.SetActive(false);
       
    }

    private void ShootSpear(InputAction.CallbackContext contxt)
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
       

        if ( Physics.Raycast(ray,out hit, 1000, spearLayer))
        {
            targetpoint = hit.point;
            Debug.Log("hit item");

            Debug.Log(hit.collider.gameObject);
        }
        else
        {
            targetpoint = ray.GetPoint(75);
        }
        Vector3 direction = targetpoint - FiringPoint.transform.position;

        Debug.Log("spear was shot!");

        if( spearAmount > 0 && !isHaunted)
        {
         audioSource.PlayOneShot(spearSound);
         spear = Instantiate(spears, FiringPoint.transform.position, FiringPoint.transform.rotation);
         spear.GetComponent<Rigidbody>().velocity = Camera.main.transform.forward * spearSpeed;
         StartCoroutine(switchSprite());
         spearAmount -= 1;
         spearAmount = Mathf.Max(spearAmount, 0);


        }
      

        
        //spear.transform.forward = Camera.main.transform.forward;



    }
    private IEnumerator switchSprite()
    {
        animator.SetBool("shooting", true);
         yield return new WaitForSeconds(0.4f);
        animator.SetBool("shooting", false);
        Debug.Log("switched");

}

   

    
}
