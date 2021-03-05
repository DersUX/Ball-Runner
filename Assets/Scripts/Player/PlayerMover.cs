using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private float _speed = 0.2f;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float inputX = _joystick.Horizontal;
        float inputY = _joystick.Vertical;

        Vector3 movement = new Vector3(inputX * _speed, 0.0f, inputY * _speed);

        if (inputX != 0 || inputY != 0)
            _rigidbody.AddForce(movement, ForceMode.VelocityChange);
    }
}
