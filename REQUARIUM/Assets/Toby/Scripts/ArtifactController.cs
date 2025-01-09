using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

[System.Serializable]
public class ArtifactController : MonoBehaviour
{
    public List<GameObject> artifactNodes;
    public GameObject targetNode;
    public GameObject artifactBase;
    public Quaternion spawnRotation = Quaternion.identity;
    public Vector3 offset;

    public bool testing;
    // Start is called before the first frame update

    [ContextMenu("FindArtifactNodes")]
    public void FindArtifactNodes()
    {
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Artifact Node");
        foreach (GameObject node in nodes)
        {
            artifactNodes.Add(node);
        }
    }

    public void CreateArtifact()
    {
        if (artifactNodes.Count == 0)
        {
            FindArtifactNodes();
        }
        List<GameObject> shuffledNodes = artifactNodes.OrderBy(x => Artifact.randyTheRandom.Next()).ToList();
        targetNode = shuffledNodes[0];
        artifactNodes.Remove(shuffledNodes[0]);
        Instantiate(artifactBase, targetNode.gameObject.transform.position + offset, spawnRotation);
    }

    public void OnEnable()
    {
        if (testing == true)
        Instantiate(artifactBase, targetNode.gameObject.transform.position + offset, spawnRotation);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
