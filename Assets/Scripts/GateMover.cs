using UnityEngine;

public class GateMover : MonoBehaviour
{
    [SerializeField] private Player _target;
    [SerializeField] private float _speed;

    private bool _gameStarted = false;

    private void Start()
    {
        _target.StartingMove += OnTargetStartingMove;
    }

    private void OnDisable()
    {
        _target.StartingMove -= OnTargetStartingMove;
    }

    private void Update()
    {
        if (_gameStarted)
            transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private void OnTargetStartingMove()
    {
        _gameStarted = true;
    }
}
