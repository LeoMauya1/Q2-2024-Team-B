using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dolphin : MonoBehaviour
{
    public enum States {Chasing, Stunned}

    private States state = States.Chasing;
    
    public GameObject player;

    public PlayerInfo playerInfo;

    private bool switchWait;

    public float stunTime;

    public float maxStunTime;

    private float leaveDistance = 0;

    private Pathfinding.Path dolphinPath;

    private float nextWaypointDistance = 3f;

    private int currentWaypoint = 0;

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

        Vector3 direction = (dolphinPath.vectorPath[currentWaypoint] - transform.position).normalized;
        rb.velocity = Vector3.Slerp(rb.velocity, direction * dolphin.speed, Time.deltaTime * slerp);
        float distance = Vector3.Distance(rb.position, dolphinPath.vectorPath[currentWaypoint]);
        //transform.forward = ghost.currentTarget.transform.position - transform.position;
        //vision._rotation = transform.rotation;
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
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
    }
}
