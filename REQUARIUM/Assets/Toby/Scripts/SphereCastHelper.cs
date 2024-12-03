using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Runtime.CompilerServices;

[Serializable]
public struct SphereCastHelper
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _radius;
    [SerializeField] public Quaternion _rotation;
    [SerializeField] private LayerMask _layerMask;

    public readonly RaycastHit[] GetHitInfo(Transform transform)
    {
        return Physics.SphereCastAll(transform.position + _offset, _radius, Vector3.zero, 1, _layerMask);
    }

    public readonly void Draw(Transform transform)
    {
        Gizmos.DrawWireSphere(transform.position + _offset, _radius);
    }

}
