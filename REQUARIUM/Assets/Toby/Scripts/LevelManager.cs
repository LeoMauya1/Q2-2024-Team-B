using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public ArtifactController artifactController;
    public EnemyManager enemyManager;
    public int artifactAmount;
    public int maxArtifacts;
    public int minArtifacts;
    public int ghostAmount;
    public int maxGhosts;
    public int minGhosts;
    public Vector3 offset;
    public bool canSpawnArtifacts;
    public bool canSpawnGhosts;

    // Start is called before the first frame update
    void Start()
    {
        SaveDataManager.Instance.daveSata.quotaMoney = 0;
        if (SaveDataManager.Instance.daveSata.isNewGame == true || SaveDataManager.Instance.daveSata.workDay == 0)
        {
            artifactAmount = 5;
            canSpawnArtifacts = true;
        }
        else if (SaveDataManager.Instance.daveSata.workDay == 1)
        {
            artifactAmount = 6;
            ghostAmount = 1;
        }
        else if (SaveDataManager.Instance.daveSata.workDay == 2)
        {
            artifactAmount = 8;
            ghostAmount = 2;
        }
        else if (SaveDataManager.Instance.daveSata.workDay >= 3)
        {
            SetBaseArtifactValue();
            SetBaseGhostValue();
        }   
    }
    public void SetBaseArtifactValue()
    {
        artifactAmount = Artifact.randyTheRandom.Next(minArtifacts, maxArtifacts);
        if (artifactAmount > 0)
        {
            canSpawnArtifacts = true;
        }
        
    }
    public void SetBaseGhostValue()
    {
        ghostAmount = Artifact.randyTheRandom.Next(minGhosts, maxGhosts);
        if (ghostAmount > 0)
        {
            canSpawnGhosts = true;
        }
    }
    void Update()
    {
        if (artifactAmount > 0)
        {
            canSpawnArtifacts = true;
        }
        if (canSpawnArtifacts == true)
        {
            artifactAmount -= 1;
            artifactController.CreateArtifact();
        }
        if (artifactAmount <= 0)
        {
            canSpawnArtifacts = false;
        }
        if (ghostAmount > 0)
        {
            canSpawnGhosts = true;
        }
        if (canSpawnGhosts == true)
        {
            ghostAmount -= 1;
            enemyManager.SpawnGhost();
        }
        if (ghostAmount <= 0)
        {
            canSpawnGhosts = false;
        }
    }
}
