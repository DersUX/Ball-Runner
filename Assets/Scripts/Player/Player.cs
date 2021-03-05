using System;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _scoreStepDistance = 50f;
    [SerializeField] private float _gameStartedDistance = 5f;
    
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private Transform _enemy;
    [SerializeField] private float _alertDistance = 8f;
    [SerializeField] private float _respawnDuration = 1.2f;
    [SerializeField] private float _respawnOffset = 15f;
    [SerializeField] private AudioSource _hitSound;
    [SerializeField] private AudioSource _ambient;

    private Rigidbody _rigidbody;

    private bool _soundEnable = true;

    private bool _gameStarted = false;

    private bool _isAttackedAlert = false;
    private bool _isAlertDistanceCurrect = false;

    private int _score;
    private int _health;
    private float _currentCoverDistance;

    private Vector3 _lastPosition;

    public float CoverDistance => transform.position.z;
    public int Score => _score;
    public int Health => _health;

    public event UnityAction StartingMove;
    public event UnityAction Dying;
    public event UnityAction<bool> EnemyAlerting;
    public event UnityAction<int> ScoreChanged;
    public event UnityAction<int> HealthChanged;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _currentCoverDistance = transform.position.z;

        _soundEnable = Convert.ToBoolean(PlayerPrefs.GetInt("Sound", 1));

        if (_soundEnable)
            _ambient.Play();
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

        _isAlertDistanceCurrect = _alertDistance >= Vector3.Distance(_enemy.position, transform.position);

        if (_isAlertDistanceCurrect && !_isAttackedAlert)
        {
            EnemyAlerting?.Invoke(true);
            _isAttackedAlert = true;
        }
        else if (!_isAlertDistanceCurrect && _isAttackedAlert)
        {
            EnemyAlerting?.Invoke(false);
            _isAttackedAlert = false;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent<PlatformMover>(out PlatformMover platformMover))
        {
            if (_soundEnable)
                _hitSound.Play();

            var effect = Instantiate(_hitEffect, collision.contacts[0].point, Quaternion.identity);

            effect.transform.rotation = Quaternion.FromToRotation(effect.transform.forward, collision.contacts[0].normal) * effect.transform.rotation;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlatformMover>(out PlatformMover platformMover))
            _lastPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DeathZone>())
            Dying?.Invoke();
    }

    public void GetComeback()
    {
        _health--;
        HealthChanged?.Invoke(_health);

        _rigidbody.Sleep();

        transform.DOMove(new Vector3(0, 1.5f, _lastPosition.z - _respawnOffset), _respawnDuration).SetUpdate(UpdateType.Normal, true).OnComplete(onComebackComplete);
    }

    private void onComebackComplete()
    {
        Time.timeScale = 1;

        StartingMove?.Invoke();
    }

    public void AddHealth()
    {
        _health++;
        HealthChanged?.Invoke(_health);
    }
}
