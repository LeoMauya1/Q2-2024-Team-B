using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Runtime.CompilerServices;

[Serializable]
public class SphereCastHelper : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _radius;
    [SerializeField] private Quaternion _rotation;
    [SerializeField] private LayerMask _layerMask;

    public RaycastHit[] GetHitInfo(Transform transform)
    {
        return Physics.SphereCastAll(transform.position + _offset, _radius, Vector3.zero);
    }

    public void Draw(Transform transform)
    {
        Gizmos.DrawWireSphere(transform.position + _offset, _radius);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
