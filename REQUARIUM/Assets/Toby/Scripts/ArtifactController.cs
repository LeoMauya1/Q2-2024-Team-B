using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ArtifactController : MonoBehaviour
{
    public List<GameObject> artifactNodes;
    public GameObject targetNode;
    public GameObject artifactBase;
    public Quaternion spawnRotation = Quaternion.identity;
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
        List<GameObject> shuffledNodes = artifactNodes; //.OrderBy(x => random.Next()).ToList();
        targetNode = shuffledNodes[0];
        artifactNodes.Remove(shuffledNodes[0]);
        Instantiate(artifactBase, targetNode.gameObject.transform.position, spawnRotation);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
