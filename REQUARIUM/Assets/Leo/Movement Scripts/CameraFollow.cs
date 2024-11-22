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

    float Yrotation;
    float xRotation;
    
    
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        


    }

    public void Aim(InputAction.CallbackContext ctx)
    {
        aimAt = ctx.ReadValue<Vector2>();
        Debug.Log(aimAt.x);
        Debug.Log(aimAt.y);


        transform.position = PlayerPos.position - offset;
        float mouseX = aimAt.x * Time.deltaTime * camSensitivty;
        float mouseY = aimAt.y * Time.deltaTime * camSensitivty;

        Yrotation += mouseX;
        xRotation -= mouseY;



        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, Yrotation, 0);
        PlayerPos.rotation = Quaternion.Euler(0, Yrotation, 0);
    }
}
