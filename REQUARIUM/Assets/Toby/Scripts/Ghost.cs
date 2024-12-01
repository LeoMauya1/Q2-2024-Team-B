using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;
using UnityEditor.Rendering;
using UnityEngine.Events;
using System.Linq;

public class Ghost : MonoBehaviour
{
    public enum States { Roaming, Watching, Attacking, Possessing, Retreating }

    public GameObject player;

    public UnityEvent possessing;

    public UnityEvent unpossess;

    private Pathfinding.Path ghostPath;
    
    private float nextWaypointDistance = 3f;
    
    private int currentWaypoint = 0;
    
    private bool reachedEndOfPath = false;

    private bool switchWait;

    private bool isPossessing;

    private bool caughtPlayer;

    public Rigidbody rb;
    
    public Seeker seeker;

    public SphereCastHelper vision;
  

    public float targetDistance;
    
    public float leaveDistance;
    
    public float speedCap;

    public float slerp;

    public float possessTime;
    
    public List<GameObject> sortedNodes;

    private States state = States.Roaming;

    public EnemyType ghost;


    [ContextMenu("FindNodes")]
    public void FindNodes()
    {
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Node");
        foreach (GameObject node in nodes)
        {
            ghost.nodes.Add(node);
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        FindNodes();
        sortedNodes = ghost.nodes.OrderBy(x => Vector3.Distance(this.transform.position, x.transform.position)).ToList();
        ghost.currentTarget = sortedNodes[0];
        ghost.nextTarget = sortedNodes[1];
    }
    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, ghost.currentTarget.transform.position, OnPathComplete);
        }
    }
    void OnPathComplete(Pathfinding.Path p)
    {
        if (!p.error)
        {
            ghostPath = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        targetDistance = Vector3.Distance(this.transform.position, ghost.currentTarget.transform.position);
        UpdatePath();
        CapVelocity(); 
        //need to put the sphere cast searching for player in here
        if (isPossessing == true)
        {
            state = States.Possessing;
        }
        if (state == States.Roaming)
        {
            IsRoaming();
        }
        else if (state == States.Watching)
        {
            IsWatching();
        }
        else if (state == States.Attacking)
        {
            IsAttacking();
        }
        else if (state == States.Possessing)
        {
            IsPossessing();
        }
    }

    [ContextMenu("IsRoaming")]
    public void IsRoaming()
    {
        if (reachedEndOfPath == true)
        {
            switchWait = true;
            sortedNodes.Remove(ghost.currentTarget);
            reachedEndOfPath = false;
            if (sortedNodes.Count == 0)
            {
                sortedNodes = ghost.nodes.OrderBy(x => Vector3.Distance(this.transform.position, x.transform.position)).ToList();
                ghost.currentTarget = sortedNodes[0];
                ghost.nextTarget = sortedNodes[1];
            }
            ghost.currentTarget = ghost.nextTarget;
            ghost.nextTarget = sortedNodes[1];
        }
        if (ghostPath == null)
        {
            return;
        }

        if (targetDistance <= leaveDistance && switchWait == false)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            switchWait = false;
            reachedEndOfPath = false;
        }

        Vector3 direction = (ghost.currentTarget.transform.position - transform.position).normalized;
        rb.velocity = Vector3.Slerp(rb.velocity, direction * ghost.speed, Time.deltaTime * slerp);
        float distance = Vector3.Distance(rb.position, ghost.currentTarget.transform.position);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        Debug.Log($"Current Waypoint: {currentWaypoint}");
    }

    [ContextMenu("IsWatching")]
    public void IsWatching()
    {
        //Have enemy recheck to see if it saw the player for sure then move to attacking
    }

    [ContextMenu("IsAttacking")]
    public void IsAttacking()
    {
        state = States.Attacking;
        ghost.currentTarget = player;


        if (ghostPath == null)
        {
            return;
        }

        if (caughtPlayer == true)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            switchWait = false;
            reachedEndOfPath = false;
        }

        Vector3 direction = (ghost.currentTarget.transform.position - transform.position).normalized;
        rb.velocity = Vector3.Slerp(rb.velocity, direction * ghost.speed, Time.deltaTime * slerp);
        float distance = Vector3.Distance(rb.position, ghost.currentTarget.transform.position);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        Debug.Log($"Current Waypoint: {currentWaypoint}");
    }

    [ContextMenu("IsPossessing")]
    public void IsPossessing()
    {
        rb.velocity = Vector3.zero;
        possessing.Invoke();
        possessTime -= Time.deltaTime;
        if (possessTime <= 0)
        {
            unpossess.Invoke();
            //respawn the enemy
            state = States.Roaming;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPossessing = true;
            caughtPlayer = true;
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
}
