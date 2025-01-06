using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    
    public List<GameObject> pNodes;
    public List<GameObject> sortedNodes;
    public int possessedNumber;
    public int artifactsGrabbed;
    public bool killedByDolphin;

    [ContextMenu("FindNodes")]
    public void FindNodes()
    {
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Node");
        foreach (GameObject node in nodes)
        {
            pNodes.Add(node);
        }
    }

    public void SortNodesBD()
    {
        sortedNodes = pNodes.OrderBy(x => Vector3.Distance(this.transform.position, x.transform.position)).ToList();
        sortedNodes.Reverse();
    }
    void Start()
    {
        FindNodes();
    }

    // Update is called once per frame
    void Update()
    {
        if (possessedNumber >= 4)
        {
            SaveDataManager.Instance.daveSata.health -= 0.05f;
        }
        if (SaveDataManager.Instance.daveSata.health <= 0 && killedByDolphin == true)
        {
            Invoke("GameOver", 3f);
        }
    }

    public void GameOver()
    {
        SaveDataManager.Instance.daveSata = SaveDataManager.Instance.defaultData;
        SceneManager.LoadScene("Game Over");
    }
}
