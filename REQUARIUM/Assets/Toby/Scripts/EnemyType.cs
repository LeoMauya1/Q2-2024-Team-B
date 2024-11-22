using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public struct EnemyType
{
    public string name;
    public float speed;
    public List<GameObject> nodes;
    public GameObject currentTarget;
    public GameObject nextTarget;
    public Animator animator;
}
