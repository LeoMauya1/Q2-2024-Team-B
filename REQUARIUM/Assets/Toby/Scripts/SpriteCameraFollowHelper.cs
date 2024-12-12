using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.UI;
public class SpriteCameraFollowHelper : MonoBehaviour
{
    void Start()
    {
      
    }
    void Update()
    {
       transform.forward = -Camera.main.transform.forward;
    }
}