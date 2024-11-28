using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ArtifactController : MonoBehaviour
{
    public List<GameObject> artifactNodes;
    public GameObject targetNode;
    public static Artifact artifactBase;
    // Start is called before the first frame update

    [ContextMenu("FindNodes")]
    public void FindNodes()
    {
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Artifact Node");
        foreach (GameObject node in nodes)
        {
            artifactNodes.Add(node);
        }
    }

    public void CreateArtifact()
    {
        List<GameObject> shuffledNodes = artifactNodes;
        targetNode = shuffledNodes[0];
        artifactNodes.Remove(shuffledNodes[0]);
        //.OrderBy(x => random.Next()).ToList();
        //Instantiate(artifactBase, targetNode.gameObject.transform, targetNode);

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
