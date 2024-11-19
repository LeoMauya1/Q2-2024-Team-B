using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody rb;
    private Vector3 movement;
    public float movementSpeed;
    public InputActionReference move;
    


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {



        movement = move.action.ReadValue<Vector3>();
        rb.velocity = new Vector3(movement.x * movementSpeed, movement.y * movementSpeed, Velocity.z);
    }
}
