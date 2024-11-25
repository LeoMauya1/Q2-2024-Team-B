using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody rb;
    private Vector3 movement;
    public float movementSpeed;
    public InputActionReference move;
    public Transform playerDirection;
    private Vector3 movedirection;



    public float playerheight;
    public LayerMask theGroud;
    public float drag;





    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        movement = move.action.ReadValue<Vector2>();
        movedirection = playerDirection.forward * movement.y + playerDirection.right * movement.x;
        rb.AddForce(movedirection.normalized * movementSpeed * 10f, ForceMode.Force);

        if (IsGrounded())
        {
            rb.drag = drag;
        }
        else
            rb.drag = 0;


    }


    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, playerheight * 0.5f + 0.2f, theGroud);

    }


}
    
       
    

   
