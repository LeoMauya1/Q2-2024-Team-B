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

    public GameObject flashlight;

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

    public bool isPossessing;

    public bool foundPlayer;

    public bool doneWatching;

    public Rigidbody rb;

    public Seeker seeker;

    public SphereCastHelper vision;


    public float targetDistance;

    public float leaveDistance;

    public float speedCap;

    public float possessedSpeedCap;

    public float possessedSpeed;

    public float slerp;

    public float possessTimeMax;

    public float possessTime;

    public float watchTimeMax;

    public float watchTime;
    
    public float followTimeMax;

    public float followTime;

    public List<GameObject> sortedNodes;

    public States state = States.Roaming;

    public EnemyType ghost;

    private float speed;

    private float spedCep;

    public Color possessColor;

    public bool isArtifact;



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
        speed = ghost.speed;
        spedCep = speedCap;
        state = States.Roaming;
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

        ghost.animator.SetBool("IsWatching", doneWatching);
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
            ghost.speed = speed;
            speedCap = spedCep;
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
        if (ghostPath == null)
        {
            return;
        }

        if (currentWaypoint >= ghostPath.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        if (targetDistance <= leaveDistance)
        {
            sortedNodes.Remove(ghost.currentTarget);
            ghost.currentTarget = ghost.nextTarget;
            ghost.nextTarget = sortedNodes[1];
            if (sortedNodes.Count == 0)
            {
                SortNodes();
            }
        }
        Vector3 direction = (ghostPath.vectorPath[currentWaypoint + 1] - transform.position).normalized;
        direction.y = 0;
        rb.velocity = Vector3.Lerp(rb.velocity, direction * ghost.speed, Time.deltaTime * slerp);
        float distance = Vector3.Distance(rb.position, ghostPath.vectorPath[currentWaypoint + 1]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        Animate(direction);
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
        rb.velocity = direction * ghost.speed;
        float distance = Vector3.Distance(rb.position, ghost.currentTarget.transform.position);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        Vector3 oldDirection = new Vector3(ghost.animator.GetFloat("AnimMoveX"), ghost.animator.GetFloat("AnimMoveY"), 0);
        Vector3 moveDirection = Vector3.Lerp(transform.forward * direction.y + transform.right * direction.x, oldDirection, 1);
        Animate(moveDirection);
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
            playerInfo.possessedNumber -= 1;
            playerInfo.SortNodesBD();
            if (isArtifact == false)
            {
                transform.position = playerInfo.sortedNodes[0].transform.position;
            }
            SortNodes();
            state = States.Roaming;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPossessing = true;
            playerInfo.possessedNumber += 1;
            flashlight = GameObject.FindGameObjectWithTag("Light");
        }
        if (other.CompareTag("FlashLight") && FLASHLIGHT.isPossessed == false)
        {
            state = States.Watching;
            watchTime = watchTimeMax + 1.5f;
        }
        else if (other.CompareTag("FlashLight") && FLASHLIGHT.isPossessed == true)
        {
            ghost.speed = possessedSpeed;
            speedCap = possessedSpeedCap;
        }
    }

    public void PossessItem()
    {
        if (playerInfo.possessedNumber == 1)
        {
            FLASHLIGHT.isPossessed = true;
            flashlight.GetComponent<Light>().color = possessColor;
        }
        else if (playerInfo.possessedNumber == 2)
        {
            SPEARGUN.isHaunted = true;
        }
        else if (playerInfo.possessedNumber >= 3)
        {
            CRUCIFISH.isHaunted = true;
        }
    }

    public void unPossessItem()
    {
        if (playerInfo.possessedNumber == 1)
        {
            FLASHLIGHT.isPossessed = false;
            flashlight.GetComponent<Light>().color = Color.white;
        }
         else if (playerInfo.possessedNumber == 2)
        {
            SPEARGUN.isHaunted = false;
        }
        else if (playerInfo.possessedNumber >= 3)
        {
            CRUCIFISH.isHaunted = false;
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

    public void Animate(Vector3 direction)
    {
        ghost.animator.SetFloat("AnimMoveX", direction.x);
        ghost.animator.SetFloat("AnimMoveY", direction.z);
    }
}
