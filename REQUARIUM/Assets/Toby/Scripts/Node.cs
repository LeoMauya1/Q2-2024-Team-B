using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Node : MonoBehaviour
{
    public Node cameFrom;
    public List<Node> connections;
    public List<Node> nodes;

    public float gScore;
    public float hScore;
    public bool drawSphere = false;
    public float FScore()
    {
        return gScore + hScore;
    }

    private void OnDrawGizmos()
    {
        if (drawSphere)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, 100);
        }
        Gizmos.color = Color.blue;

        if (connections.Count > 0)
        {
            for (int i = 0; i < connections.Count; i++)
            {
                Gizmos.DrawLine(transform.position, connections[i].transform.position);
            }
        }
    }
    [ContextMenu("ConnectNodes")]
    public void ConnectNodes()
    {
        connections = FindObjectsOfType<Node>().Where(node => Vector3.Distance(transform.position, node.transform.position) <= 100).ToList();
    }

    [ContextMenu("DrawSphere")]
    public void DrawSphere()
    {
        drawSphere = true;
    }
    [ContextMenu("DeleteSphere")]
    public void DeleteSphere()
    {
        drawSphere = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
