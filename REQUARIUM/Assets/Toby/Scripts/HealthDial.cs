using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDial : MonoBehaviour
{
    public RectTransform rotatePoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotatePoint.localRotation = Quaternion.Euler(0f, 0f, SaveDataManager.Instance.daveSata.health);
    }
}
