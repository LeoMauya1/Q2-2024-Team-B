using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class CameraFollow : MonoBehaviour
{
    public float camSensitivty;
    public float camDistance;
    public Transform PlayerPos;
    public Vector3 offset;
    private Vector2 aimAt;

   public static float Yrotation;
   public static float xRotation;


    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = PlayerPos.position - offset;
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * camSensitivty;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * camSensitivty;

        Yrotation += mouseX;
        xRotation -= mouseY;



        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, Yrotation, 0);
       // Debug.Log(transform.rotation.x);
       // Debug.Log(transform.rotation.y);
        PlayerPos.rotation = Quaternion.Euler(0, Yrotation, 0);
    }
}



    

     



