using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody rb;
    private Vector3 movement;
    private float movementSpeed;
    public InputActionReference move;
    public Transform playerDirection;
    private Vector3 movedirection;
    public InputActionReference crouch;
    public InputActionReference Jump;
    public Transform crouchPos;
    public InputActionReference sprint;


    public float playerheight;
    public LayerMask theGroud;
    public float drag;
    //private float isCrouch;

    public float jumpPower;
    public float jumpCooldown;
    public float airMultiplier;
    private bool readyToJump;
    public MovementState state;
    public float walkSpeed;
    public float SprintSpeed;
    private float playerOriginalPos;
    public float crouchSpeed;
    public float crouchScale;
    
   

    public enum MovementState
    {
        walking,
        sprint,
        air,
        crouching
    }


    void Start()
    {
        readyToJump = true;
        playerOriginalPos = transform.localScale.y;
    } 

    // Update is called once per frame
    void Update()
    {
        movement = move.action.ReadValue<Vector2>();
        movedirection = playerDirection.forward * movement.y + playerDirection.right * movement.x;


        if (crouch.action.IsPressed())
        {
           transform.localScale = new Vector3(transform.localScale.x, crouchScale, transform.localScale.z);
            Debug.Log("ur Crouching");
           rb.AddForce(Vector3.down * 5, ForceMode.Impulse);
        }
      
        if (crouch.action.WasReleasedThisFrame())
        {
            transform.localScale = new Vector3(transform.localScale.x,playerOriginalPos, transform.localScale.z);
        }
   
        if(Jump.action.WasPerformedThisFrame() && IsGrounded() && readyToJump)
        {
            readyToJump = false;
            Jumping();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
        if(IsGrounded())
        {
          rb.AddForce(movedirection.normalized * movementSpeed * 10f, ForceMode.Force);

        }
         else if (!IsGrounded())
        {
            rb.AddForce(movedirection.normalized * movementSpeed * 10f * airMultiplier, ForceMode.Force);
        }
        
        speedControl();
        StateHandler();

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

    private void speedControl()
    {
        Vector3 flatvel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatvel.magnitude > movementSpeed)
        {
            Vector3 limitVel = flatvel.normalized * movementSpeed;
            rb.velocity = new Vector3(limitVel.x, rb.velocity.y, limitVel.z);

        }
    }

    private void StateHandler()
    {
        if(crouch.action.IsPressed())
        {
            state = MovementState.crouching;
            movementSpeed = crouchSpeed;
        }
        else if( IsGrounded() && sprint.action.IsPressed())
        {
            state = MovementState.sprint;
            movementSpeed = SprintSpeed;
        } 
        else if (IsGrounded())
        {
            state = MovementState.walking;
            movementSpeed = walkSpeed;
            
        }
        else
        {
            state = MovementState.air;
        }
        
    }



    private void Jumping()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up *jumpPower*Time.deltaTime, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

}
    
       
    

   
