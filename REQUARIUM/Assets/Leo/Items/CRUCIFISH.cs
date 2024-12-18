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

    private float elapsedTime = 0;
    public Vector3 HitBoxPosition;
    public Vector3 HitBoxRotation;

    private void Update()
    {
        hitBox.transform.position = Camera.main.transform.position + Camera.main.transform.TransformDirection(HitBoxPosition);
        hitBox.transform.rotation = Camera.main.transform.rotation * Quaternion.Euler(HitBoxRotation);


    }

    private void Start()
    {
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
       if(canSwing == false)
        {
            StartCoroutine(SwingCooldown());
        }
        
        if( canSwing == true)
        {
         StartCoroutine(swinging());

        }
    }

    private IEnumerator swinging()


    {
        canSwing = false;
        
        while (elapsedTime < CrucifishSwingTime)
        {
            transform.position = Vector3.Lerp(crucifishOriginalPosition.position, SwingTo.position, elapsedTime / CrucifishSwingTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(SwingTo.position - transform.position), elapsedTime / CrucifishSwingTime) * Quaternion.Euler(swingRotation);

            elapsedTime += Time.deltaTime;
            yield return null;  
        }

        StartCoroutine(HitBox());
        elapsedTime = 0f;

       
        while (elapsedTime < CrucifishSwingTime)
        {
            transform.position = Vector3.Lerp(SwingTo.position, crucifishOriginalPosition.position, elapsedTime / CrucifishSwingTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(SwingTo.position - transform.position), elapsedTime / CrucifishSwingTime) * Quaternion.Euler(swingRotation);

            elapsedTime += Time.deltaTime;
            yield return null; 
        }
        elapsedTime = 0f;
        

        transform.position = crucifishOriginalPosition.position;
        transform.rotation = crucifishOriginalRotation;


    }

    private IEnumerator SwingCooldown()
    {
        yield return new WaitForSeconds(2f);
        canSwing = true;
    }

    private IEnumerator HitBox()
    {
        hitBox.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        hitBox.SetActive(false);
    }

  

}
