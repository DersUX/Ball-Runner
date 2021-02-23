using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 0.2f;
    [SerializeField] private float _scoreStepDistance = 50f;
    [SerializeField] private float _gameStartedDistance = 5f;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private ParticleSystem _hitEffect;

    private Rigidbody _rigidbody;
    private AudioSource _audio;
    private bool _soundOn = true;
    private int _score;
    private float _currentCoverDistance;

    private bool _gameStarted = false;

    public float CoverDistance => transform.position.z;

    public event UnityAction StartingMove;
    public event UnityAction<int> ScoreChanged;
    public event UnityAction<int> Dying;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audio = GetComponent<AudioSource>();

        _currentCoverDistance = transform.position.z;

        _soundOn = Convert.ToBoolean(PlayerPrefs.GetInt("Sound", 1));
    }

    private void Update()
    {
        if (!_gameStarted && transform.position.z > _gameStartedDistance)
        {
            StartingMove?.Invoke();
            _gameStarted = true;
        }

        if (_currentCoverDistance + _scoreStepDistance < transform.position.z)
        {
            _currentCoverDistance = transform.position.z;

            _score++;
            ScoreChanged?.Invoke(_score);
        }
    }

    private void FixedUpdate()
    {
        float inputX = _joystick.Horizontal;
        float inputY = _joystick.Vertical;

        Vector3 movement = new Vector3(inputX * _speed, 0.0f, inputY * _speed);

        if (inputX != 0 || inputY != 0)
            _rigidbody.AddForce(movement, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent<PlatformMover>(out PlatformMover platformMover))
        {
            if (_soundOn)
                _audio.Play();

            var effect = Instantiate(_hitEffect, collision.contacts[0].point, Quaternion.identity);

            effect.transform.rotation = Quaternion.FromToRotation(effect.transform.forward, collision.contacts[0].normal) * effect.transform.rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DeathZone>())
            Dying?.Invoke(_score);
    }
}
