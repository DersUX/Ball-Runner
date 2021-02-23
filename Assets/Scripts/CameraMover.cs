using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private GameObject _target;

    private Vector3 _offset;

    private void Start()
    {
        _offset = transform.position - _target.transform.position;
    }

    private void LateUpdate()
    {
        transform.position = _target.transform.position + _offset;
    }
}
