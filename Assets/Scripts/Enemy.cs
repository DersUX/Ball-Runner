using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Player _target;
    [SerializeField] private Vector3 _kickDirection;
    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private float _kickDistance = 0.2f;
    [SerializeField] private float _maxDistance = 10;

    private Animator _animator;

    private float _currentDistance;
    private bool _gameStarted = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _target.StartingMove += OnTargetStartingMove;
    }

    private void OnDisable()
    {
        _target.StartingMove -= OnTargetStartingMove;
    }

    private void Update()
    {
        if (_gameStarted)
        {
            _currentDistance = Vector3.Distance(transform.position, _target.transform.position);

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
        Vector3 currentPosition = new Vector3(_target.transform.position.x, transform.position.y, _target.transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, currentPosition, speed * Time.deltaTime);
    }

    private void Kick()
    {
        _animator.Play("Kick");

        Vector3 currentKickDirection = _kickDirection;
        currentKickDirection.x = Random.Range(-_kickDirection.x, _kickDirection.x);

        _target.GetComponent<Rigidbody>().AddForce(currentKickDirection);
    }

    private void OnTargetStartingMove()
    {
        _gameStarted = true;
    }
}
