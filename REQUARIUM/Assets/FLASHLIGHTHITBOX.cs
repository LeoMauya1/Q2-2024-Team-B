using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FLASHLIGHTHITBOX : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("BEGONE GHOST!!!");
    }
}
