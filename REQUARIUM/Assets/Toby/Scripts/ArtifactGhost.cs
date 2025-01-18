using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArtifactGhost : MonoBehaviour
{
    public UnityEvent possessing;

    public UnityEvent unpossess;

    public bool isPossessing;

    public float possessTimeMax;

    public float possessTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnDisable()
    {
        FLASHLIGHT.isPossessed = false;
        SPEARGUN.isHaunted = false;
        CRUCIFISH.isHaunted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPossessing == true)
        {
            IsPossessing();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            FLASHLIGHT.isPossessed = true;
            PlayerInfo.possessedNumber += 1;
            Debug.Log($"{SaveDataManager.Instance.daveSata.health}");
        }
    }
    public void PossessItem()
    {
        if (PlayerInfo.possessedNumber == 1)
        {
            FLASHLIGHT.isPossessed = true;
        }
        else if (PlayerInfo.possessedNumber == 2)
        {
            SPEARGUN.isHaunted = true;
        }
        else if (PlayerInfo.possessedNumber >= 3)
        {
            CRUCIFISH.isHaunted = true;
        }
    }

    public void unPossessItem()
    {
        if (PlayerInfo.possessedNumber == 1)
        {
            FLASHLIGHT.isPossessed = false;
        }
        else if (PlayerInfo.possessedNumber == 2)
        {
            SPEARGUN.isHaunted = false;
        }
        else if (PlayerInfo.possessedNumber >= 3)
        {
            CRUCIFISH.isHaunted = false;
        }
    }
    public void IsPossessing()
    {
        possessing.Invoke();
        possessTime -= Time.deltaTime;
        if (possessTime <= 0 && isPossessing == true)
        {
            unpossess.Invoke();
            isPossessing = false;
            PlayerInfo.possessedNumber -= 1;
            FindObjectOfType<PlayerInfo>().possessionClip.volume -= 0.1f;
        }
    }
}
