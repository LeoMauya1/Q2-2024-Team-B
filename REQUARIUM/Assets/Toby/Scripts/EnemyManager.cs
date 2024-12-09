using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> ghosts;
    private GameObject ghost;
    public List<GameObject> ghostSpawnPoints;
    private GameObject spawnPoint;
    public GameObject dolphinSpawn;
    public GameObject dolphin;
    public float dolphinTimer;
    public float maxDolphinTime;
    public bool dolphinSpawned;
    private Quaternion spawnRotation = Quaternion.identity;
    void Start()
    {
        SpawnGhost();
        dolphinTimer = maxDolphinTime;
    }
    public void RandomizeGhost()
    {
        System.Random rand = new System.Random();
        List<GameObject> shuffledGhosts = ghosts.OrderBy(x => rand.Next()).ToList();
        ghost = shuffledGhosts[0];
    }

    public void RandomizeSpawn()
    {
        System.Random rand = new System.Random();
        List<GameObject> shuffledNodes = ghostSpawnPoints.OrderBy(x => rand.Next()).ToList();
        spawnPoint = shuffledNodes[0];
    }

    [ContextMenu("Spawn Ghost")]
    public void SpawnGhost()
    {
        RandomizeGhost();
        RandomizeSpawn();
        Instantiate(ghost, spawnPoint.gameObject.transform.position, spawnRotation);
    }

    public void SpawnDolphin()
    {
        dolphinSpawned = true;
        Instantiate(dolphin, dolphinSpawn.gameObject.transform.position, spawnRotation);
    }

    void Update()
    {
        if (dolphinTimer > 0)
        {
            dolphinTimer -= Time.deltaTime;
        }
        else if (dolphinTimer <= 0 && dolphinSpawned == false)
        {
            SpawnDolphin();
        }
    }
    [ContextMenu("Find Spawn Points")]
    public void FindGhostNodes()
    {
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Ghost Node");
        foreach (GameObject node in nodes)
        {
            ghostSpawnPoints.Add(node);
        }
    }
}
