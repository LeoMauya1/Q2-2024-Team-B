using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;
using UnityEditor.Rendering;
using UnityEngine.Events;
using System.Linq;
using System.ComponentModel.Design;
using System;
using UnityEngine.EventSystems;

public class Dolphin : MonoBehaviour
{
    public enum States {Chasing, Stunned}

    private States state = States.Chasing;
    
    public GameObject player;

    public PlayerInfo playerInfo;

    private bool switchWait;

    public float stunTime;

    public float maxStunTime;

    private float leaveDistance = 10;

    private Pathfinding.Path dolphinPath;

    private float nextWaypointDistance = 3f;

    public int currentWaypoint = 10;

    private bool reachedEndOfPath = false;

    public Rigidbody rb;

    public Seeker seeker;

    public float speedCap;

    public float slerp;

    public float targetDistance;

    public EnemyType dolphin;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInfo = player.GetComponent<PlayerInfo>();
        dolphin.currentTarget = player;
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, dolphin.currentTarget.transform.position, OnPathComplete);
        }
    }

    void OnPathComplete(Pathfinding.Path p)
    {
        if (!p.error)
        {
            dolphinPath = p;
            currentWaypoint = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {
        targetDistance = Vector3.Distance(this.transform.position, dolphin.currentTarget.transform.position);
        UpdatePath();
        CapVelocity();
        if (state == States.Chasing)
        {
            IsChasing();
        }
        else if (state == States.Stunned)
        {
            IsStunned();
        }
        dolphin.animator.SetFloat("Velocity", rb.velocity.magnitude);
    }

    public void IsChasing()
    {
        if (dolphinPath == null)
        {
            return;
        }

        if (currentWaypoint >= dolphinPath.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        Debug.Log($"Direction: {dolphinPath.vectorPath[currentWaypoint]}");
        Vector3 direction = (dolphinPath.vectorPath[currentWaypoint + 1] - transform.position).normalized;
        rb.velocity = Vector3.Lerp(rb.velocity, direction * dolphin.speed, Time.deltaTime * slerp);
        float distance = Vector3.Distance(rb.position, dolphinPath.vectorPath[currentWaypoint + 1]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        Vector3 oldDirection = new Vector3(dolphin.animator.GetFloat("AnimMoveX"), dolphin.animator.GetFloat("AnimMoveY"), 0);
        Vector3 moveDirection = Vector3.Lerp(transform.forward * direction.y + transform.right * direction.x, oldDirection, 1);
        Animate(moveDirection);
    }
    public void IsStunned()
    {
        rb.velocity = Vector3.zero;
        stunTime -= Time.deltaTime;
        if (stunTime <= 0)
        {
            state = States.Chasing;
        }    
    }
    public void CapVelocity()
    {
        if (rb.velocity.x < -speedCap)
        {
            rb.velocity = new(-speedCap, rb.velocity.y);
            if (rb.velocity.z < -speedCap)
            {
                rb.velocity = new(-speedCap, rb.velocity.y, -speedCap);
            }
        }
        if (rb.velocity.z < -speedCap)
        {
            rb.velocity = new(rb.velocity.x, rb.velocity.y, -speedCap);
            if (rb.velocity.x < -speedCap)
            {
                rb.velocity = new(-speedCap, rb.velocity.y, -speedCap);
            }
        }
        else if (rb.velocity.x > speedCap)
        {
            rb.velocity = new(speedCap, rb.velocity.y);
            if (rb.velocity.z > speedCap)
            {
                rb.velocity = new(speedCap, rb.velocity.y, speedCap);
            }
        }
        else if (rb.velocity.z > speedCap)
        {
            rb.velocity = new(rb.velocity.x, rb.velocity.y, speedCap);
            if (rb.velocity.z > speedCap)
            {
                rb.velocity = new(speedCap, rb.velocity.y, speedCap);
            }
        }
    }
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            stunTime = maxStunTime;
            playerInfo.saveData.health -= 50;
            //stunned animation
            state = States.Stunned;
        }
        else if (other.gameObject.CompareTag("Spear"))
        {
            stunTime = maxStunTime;
            state = States.Stunned;
        }
    }

    public void Animate(Vector3 direction)
    {
        dolphin.animator.SetFloat("AnimMoveX", direction.x);
        dolphin.animator.SetFloat("AnimMoveY", direction.z);
    }
}
