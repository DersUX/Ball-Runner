using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct GameObjectsList
{
    public List<GameObject> ObjectsList;
    public float _startSpawnDistance;
}

public abstract class Spawner : ObjectPool
{
    [SerializeField] protected Transform _point;
    [SerializeField] protected Player _player;

    [SerializeField] protected float _spawnDelay = 1;
    [SerializeField] protected float _spawnStepDistance = 50;
    [SerializeField] protected float _minSpawnDistance = 10;
    [SerializeField] protected float _maxSpawnDistance = 20;

    [SerializeField] private GameObjectsList[] _gameObjectsLists;

    protected float _currentTime = 0;
    protected float _currentSpawnDistance;

    protected static Vector3 _currentPosition;

    private void Awake()
    {
        Initialize(_gameObjectsLists[0].ObjectsList);

        _currentPosition = _point.position;
        _currentSpawnDistance = _spawnStepDistance;
    }

    protected void TrySetGameObject(Vector3 point)
    {
        _currentTime += Time.deltaTime;

        if (_currentTime >= _spawnDelay && TryGetObject(out GameObject gameObject))
        {
            _currentTime = 0;

            SetGameObject(gameObject, point);
        }
    }

    protected void SetGameObject(GameObject gameObject, Vector3 spawnPoint)
    {
        _currentPosition.z += Random.Range(_minSpawnDistance, _maxSpawnDistance);

        gameObject.SetActive(true);
        for (int i = 0; i < gameObject.transform.childCount; i++)
            gameObject.transform.GetChild(i).gameObject.SetActive(true);

        gameObject.transform.position = spawnPoint;
    }

    protected void TryGetNewGameObject()
    {
        if (_currentSpawnDistance < _player.CoverDistance)
        {
            _currentSpawnDistance += _spawnStepDistance;

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
