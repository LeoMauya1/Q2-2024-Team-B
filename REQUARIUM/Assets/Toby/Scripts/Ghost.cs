using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;
using UnityEditor.Rendering;
using System.Linq;

public class Ghost : MonoBehaviour
{
    public enum States { Roaming, Watching, Attacking, Possessing, Retreating }
    
    
    private Pathfinding.Path ghostPath;
    
    private float nextWaypointDistance = 3f;
    
    private int currentWaypoint = 0;
    
    private bool reachedEndOfPath = false;

    private bool switchWait;

    public Rigidbody rb;
    
    public Seeker seeker;

    public SphereCastHelper vision;
  

    public float targetDistance;
    
    public float leaveDistance;
    
    public float speedCap;

    public float slerp;
    
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
    }

    [ContextMenu("IsWatching")]
    public void IsWatching()
    {

    }

    [ContextMenu("IsAttacking")]
    public void IsAttacking()
    {

    }

    [ContextMenu("IsPossessing")]
    public void IsPossessing()
    {

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
