using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class CRUCIFISH : MonoBehaviour
{
    public PLAYERCONTROLLER playerInputs;
    public InputAction crucifishEvent;
    public Transform SwingTo;
    public Quaternion swingRotation;
    public float swingSpeed;
    private Transform crucifishOriginalPosition;
    private Quaternion crucifishOriginalRotation;
    public float crucifishLagTime;

    public static bool isHaunted;







    private void Start()
    {
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
        StartCoroutine(swinging());
    }

    private IEnumerator swinging()


    {
        Debug.Log("crucified!");
        transform.position = Vector3.Lerp(transform.position, SwingTo.position, swingSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(crucifishOriginalRotation, Quaternion.LookRotation(SwingTo.position - transform.position), swingSpeed * Time.deltaTime);
        yield return new WaitForSeconds(crucifishLagTime);
        transform.position = Vector3.Lerp(SwingTo.position, crucifishOriginalPosition.position, swingSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.RotateTowards(SwingTo.position, crucifishOriginalPosition.position, 3, 4));
    }

}
