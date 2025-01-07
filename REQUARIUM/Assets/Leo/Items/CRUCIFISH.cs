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
    public GameObject hitBox;

    public static bool isHaunted;

    public float elapsedTime = 0;
    public Vector3 HitBoxPosition;
    public Vector3 HitBoxRotation;

    private Animator animator;
    private AudioSource audioSource;
    public AudioClip crucifishAudio;

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
    }

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        
        crucifishEvent = playerInputs.GamePlay1.Crucifish;
        crucifishEvent.Enable();
        crucifishEvent.performed += swingCrucifish;

    }
    private void OnDisable()
    {
        crucifishEvent.Disable();
    }

    private void swingCrucifish(InputAction.CallbackContext contxt)
    {
        StartCoroutine(SwingingProcess());
    }

    private void swinging()
    {
       
         
            playSound();
            animator.SetBool("isSwinging", true);
            
  
        
    }

    private void returnSwing()
    {
        animator.SetBool("isSwinging", false);
    }

    private IEnumerator HitBox()
    {
        hitBox.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        hitBox.SetActive(false);
    }

    private void playSound()
    {
        if( audioSource.isPlaying)
        {
            return;
        }

        audioSource.PlayOneShot(crucifishAudio);
    }


    private IEnumerator SwingingProcess()
    {
        swinging();
        yield return new WaitForSeconds(1.3f);
        returnSwing();
    }
}
