using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class CRUCIFISH : MonoBehaviour
{
    public PLAYERCONTROLLER playerInputs;
    public InputAction crucifishEvent;
    public Transform SwingTo;
    public Vector3 swingRotation;
    public float swingSpeed;
    private Transform crucifishOriginalPosition;
    private Quaternion crucifishOriginalRotation;
    public float CrucifishSwingTime;
    private bool canSwing = true;
    public BoxCollider hitBox;

    public static bool isHaunted;

    public float elapsedTime = 0;
    public Vector3 HitBoxPosition;
    public Vector3 HitBoxRotation;

    private Animator animator;
    private AudioSource audioSource;
    public AudioClip crucifishAudio;
    private GameObject crucifishTut;

    private void Update()
    {
        hitBox.transform.position = Camera.main.transform.position + Camera.main.transform.TransformDirection(HitBoxPosition);
        hitBox.transform.rotation = Camera.main.transform.rotation * Quaternion.Euler(HitBoxRotation);


    }

    private void Start()
    {
        
        animator = GetComponent<Animator>();
        hitBox.transform.SetParent(null);
        crucifishOriginalPosition = transform;
        crucifishOriginalRotation = transform.rotation;


    }
    private void Awake()
    {
        playerInputs = new PLAYERCONTROLLER();
        crucifishTut = GameObject.Find("CRUCIFISH TEXT");
    }

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        
        crucifishEvent = playerInputs.GamePlay1.Crucifish;
        crucifishEvent.Enable();
        crucifishEvent.performed += swingCrucifish;
        crucifishTut.SetActive(true);

    }
    private void OnDisable()
    {
        crucifishEvent.Disable();
        crucifishTut.SetActive(false);
    }

    private void swingCrucifish(InputAction.CallbackContext contxt)
    {
        StartCoroutine(SwingingProcess());
    }

    private void swinging()
    {
       
         
            animator.SetBool("isSwinging", true);
            playSound();
            StartCoroutine(HitBox());
            
  
        
    }

    private void returnSwing()
    {
        animator.SetBool("isSwinging", false);
    }

    private IEnumerator HitBox()
    {
        yield return new WaitForSeconds(0.2f);
        hitBox.enabled = true;
        
        yield return new WaitForSeconds(0.5f);
        hitBox.enabled = false;
    }

    private void playSound()
    {
       

        audioSource.PlayOneShot(crucifishAudio);
    }


    private IEnumerator SwingingProcess()
    {
        swinging();
        yield return new WaitForSeconds(.3f);
        returnSwing();
    }
}
