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
    [SerializeField] private QueryTriggerInteraction _trigger;

    public readonly bool GetHitInfo(Transform transform, out RaycastHit hitInfo)
    {
        return Physics.SphereCast(transform.position + _offset, _radius, Vector3.one, out hitInfo, Mathf.Infinity, _layerMask, _trigger);
    }

    public readonly void Draw(Transform transform)
    {
        Gizmos.DrawWireSphere(transform.position + _offset, _radius);
    }

}
