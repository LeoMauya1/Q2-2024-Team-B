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
    private bool reachedEndOfPath = false;

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

        if (currentWaypoint >= ghostPath.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector3 direction = ((Vector3)ghostPath.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector3 force = direction * ghost.speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector3.Distance(rb.position, ghostPath.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    [ContextMenu("IsRoaming")]
    public void IsRoaming()
    {

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
