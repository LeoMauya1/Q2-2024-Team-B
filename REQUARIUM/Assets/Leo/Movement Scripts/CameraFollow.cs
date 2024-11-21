using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    public float camSensitivty;
    public float camDistance;
    public Transform PlayerPos;
    public Vector3 offset;

    float Yrotation;
    float xRotation;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = PlayerPos.position - offset;
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * camSensitivty;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * camSensitivty;

        Yrotation += mouseX;
        xRotation -= mouseY;



        xRotation = Mathf.Clamp(xRotation, -90f,90f);

        transform.rotation = Quaternion.Euler(xRotation, Yrotation, 0);
        PlayerPos.rotation = Quaternion.Euler(0,Yrotation,0);


    }
}
