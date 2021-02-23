using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct GameObjectsList
{
    public List<GameObject> ObjectsList;
    public float _startSpawnDistance;
}

public class Spawner : ObjectPool
{
    [SerializeField] private Transform _point;
    [SerializeField] private Player _player;
    [SerializeField] private float _minSpawnDistance = 10;
    [SerializeField] private float _maxSpawnDistance = 20;
    [SerializeField] private float _secondsBetweenSpawn = 1;
    [SerializeField] private float _difficultyIncreaseStep = 100;
    [SerializeField] private int _maxCapacity = 40;
    [SerializeField] private GameObjectsList[] _gameObjectsLists;

    private float _currentTime = 0;
    private float _currentDifficultyStep = 0;
    private Vector3 _currentPosition;

    private void Start()
    {
        Initialize(_gameObjectsLists[0].ObjectsList);

        _currentPosition = _point.position;

        for (int i = 0; TryGetObject(out GameObject obstacle); i++)
            SetObstacle(obstacle, _currentPosition);

        _currentDifficultyStep = _difficultyIncreaseStep;
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime >= _secondsBetweenSpawn && TryGetObject(out GameObject obstacle))
        {
            _currentTime = 0;

            SetObstacle(obstacle, _currentPosition);
        }

        TryGetNewGameObject();
    }

    private void SetObstacle(GameObject obstacle, Vector3 spawnPoint)
    {
        _currentPosition.z += Random.Range(_minSpawnDistance, _maxSpawnDistance);

        obstacle.SetActive(true);
        for (int i = 0; i < obstacle.transform.childCount; i++)
            obstacle.transform.GetChild(i).gameObject.SetActive(true);

        obstacle.transform.position = spawnPoint;
    }

    private void TryGetNewGameObject()
    {
        if (_currentDifficultyStep < _player.CoverDistance && _maxCapacity > PoolCount)
        {
            _currentDifficultyStep += _difficultyIncreaseStep;

            GameObject gameObject;

            for (int i = _gameObjectsLists.Length - 1; i > 0; i--)
            {
                if (_gameObjectsLists[i]._startSpawnDistance < _player.CoverDistance)
                {
                    gameObject = _gameObjectsLists[i].ObjectsList[Random.Range(0, _gameObjectsLists[i].ObjectsList.Count)];

                    AddObjectToList(gameObject);
                    return;
                }
            }
        }
    }
}
