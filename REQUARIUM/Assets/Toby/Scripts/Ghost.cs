using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;
using UnityEditor.Rendering;
using UnityEngine.Events;
using System.Linq;
using System.ComponentModel.Design;

public class Ghost : MonoBehaviour
{
    public enum States { Roaming, Watching, Attacking, Possessing, Retreating }

    public GameObject player;

    public float playerDistance;

    public float watchDistance;

    public PlayerInfo playerInfo;

    public UnityEvent possessing;

    public UnityEvent unpossess;

    private Pathfinding.Path ghostPath;

    private float nextWaypointDistance = 3f;

    private int currentWaypoint = 0;

    private bool reachedEndOfPath = false;

    private bool switchWait;

    private bool isPossessing;

    public bool foundPlayer;

    public bool doneWatching;

    public Rigidbody rb;

    public Seeker seeker;

    public SphereCastHelper vision;


    public float targetDistance;

    public float leaveDistance;

    public float speedCap;

    public float slerp;

    public float possessTimeMax;

    public float possessTime;

    public float watchTimeMax;

    public float watchTime;
    
    public float followTimeMax;

    public float followTime;

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
        playerInfo = player.GetComponent<PlayerInfo>();
        FindNodes();
        SortNodes();
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
        Debug.Log($"Current Target: {ghost.currentTarget}");
        Debug.Log($"Current State: {state}");
        targetDistance = Vector3.Distance(this.transform.position, ghost.currentTarget.transform.position);
        playerDistance = Vector3.Distance(this.transform.position, player.transform.position);
        UpdatePath();
        CapVelocity();
        if (playerDistance <= watchDistance)
        {
            foundPlayer = true;
        }
        else
        {
            foundPlayer = false;
        }
        if (isPossessing == true)
        {
            state = States.Possessing;
        }
        if (foundPlayer == true && doneWatching == false)
        {
            state = States.Watching;
        }
        if (state == States.Roaming)
        {
            watchTime = watchTimeMax;
            doneWatching = false;
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
        GetInfo();
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
        //transform.forward = ghost.currentTarget.transform.position - transform.position;
        //vision._rotation = transform.rotation;
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    [ContextMenu("IsWatching")]
    public void IsWatching()
    {
        rb.velocity = Vector3.zero;
        GetInfo();
    }

    [ContextMenu("IsAttacking")]
    public void IsAttacking()
    {
        GetInfo();

        if (ghostPath == null)
        {
            return;
        }

        if (isPossessing == true)
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
        //transform.forward = ghost.currentTarget.transform.position - transform.position;
        //vision._rotation = transform.rotation;
        float distance = Vector3.Distance(rb.position, ghost.currentTarget.transform.position);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    [ContextMenu("IsPossessing")]
    public void IsPossessing()
    {
        rb.velocity = Vector3.zero;
        possessing.Invoke();
        possessTime -= Time.deltaTime;
        if (possessTime <= 0 && isPossessing == true)
        {
            unpossess.Invoke();
            isPossessing = false;
            playerInfo.SortNodesBD();
            transform.position = playerInfo.sortedNodes[0].transform.position;
            SortNodes();
            state = States.Roaming;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPossessing = true;
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

    public void SortNodes()
    {
        sortedNodes = ghost.nodes.OrderBy(x => Vector3.Distance(this.transform.position, x.transform.position)).ToList();
        ghost.currentTarget = sortedNodes[0];
        ghost.nextTarget = sortedNodes[1];
    }

    public void GetInfo()
    {
        if (state == States.Roaming && foundPlayer == true)
        {
            state = States.Watching;
        }
        if (state == States.Watching)
        {
            if (watchTime > 0)
            {
                watchTime -= Time.deltaTime;
            }
            else if (watchTime <= 0)
            {
                doneWatching = true;
                ghost.currentTarget = player;
                state = States.Attacking;
            }
        }
        if (state == States.Attacking)
        {
            if (foundPlayer == false)
            {
                followTime -= Time.deltaTime;
                if (followTime <= 0)
                {
                    SortNodes();
                    state = States.Roaming;
                }
            } 
            else
            {
                followTime = followTimeMax;
            }
            
        }
    }
    public void OnDrawGizmos()
    {
        vision.Draw(transform);
    }
}
