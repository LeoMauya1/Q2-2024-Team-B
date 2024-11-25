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
    
    public EnemyType ghost;
    
    private Pathfinding.Path ghostPath;
    
    public float nextWaypointDistance = 3f;
    
    public int currentWaypoint = 0;
    
    public bool reachedEndOfPath = false;

    public Rigidbody rb;
    
    public Seeker seeker;
  

    public float targetDistance;
    
    public float leaveDistance;
    
    public List<GameObject> sortedNodes;

    public States state = States.Roaming;


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
        else if (state == States.Retreating)
        {
            IsRetreating();
        }
        
        if (ghostPath == null)
        {
            return;
        }

        if (targetDistance <= leaveDistance)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector3 direction = (ghost.currentTarget.transform.position - transform.position).normalized;
        Debug.Log($"Direction: {direction}");
        Vector3 force = direction * ghost.speed * Time.deltaTime;
        Vector3 newForce = new Vector3(force.x, force.y, force.z);
        rb.AddForce(newForce);
        Debug.Log($"Newforce: {newForce}");
        float distance = Vector3.Distance(rb.position, ghost.currentTarget.transform.position);
        Debug.Log($"Distance: {distance}");
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
            sortedNodes.Remove(ghost.currentTarget);
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

    [ContextMenu("IsRetreating")]
    public void IsRetreating()
    {

    }
}
