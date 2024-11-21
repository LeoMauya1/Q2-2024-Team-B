using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class Enemy
{
    [System.Serializable]
    public struct EnemyType
    {
        public string name;
        public float speed;
        public OnCollisionEnter collision;
        public List<Node> nodes;
        public Node currentNode;
        public Node nextNode;
        public Animator animator;
    }
}
