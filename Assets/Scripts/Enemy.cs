using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Vector3 _kickDirection;
    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private float _kickDistance = 0.2f;
    [SerializeField] private float _maxDistance = 15;

    private Animator _animator;

    private Vector3 _startPosition;
    private bool _playerMoving = false;
    private float _currentDistance;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _player.StartingMove += OnPlayerStartingMove;
        _player.Dying += OnPlayerDying;

        _startPosition = transform.position;
    }

    private void OnDisable()
    {
        _player.StartingMove -= OnPlayerStartingMove;
        _player.Dying -= OnPlayerDying;
    }

    private void Update()
    {
        if (_playerMoving)
        {
            _currentDistance = Vector3.Distance(transform.position, _player.transform.position);

            if (_maxDistance < _currentDistance)
                Move(_moveSpeed * 10);
            else
                Move(_moveSpeed);

            if (_currentDistance <= _kickDistance)
                Kick();
        }
    }

    private void Move(float speed)
    {
        Vector3 currentPosition = new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, currentPosition, speed * Time.deltaTime);
    }

    private void Kick()
    {
        _animator.Play("Kick");

        Vector3 currentKickDirection = _kickDirection;
        currentKickDirection.x = Random.Range(-_kickDirection.x, _kickDirection.x);

        _player.GetComponent<Rigidbody>().AddForce(currentKickDirection);
    }

    private void OnPlayerStartingMove()
    {
        _playerMoving = true;
    }

    private void OnPlayerDying()
    {
        _playerMoving = false;
        transform.position = _startPosition;
    }
}
