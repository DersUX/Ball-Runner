using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : Spawner
{
    [SerializeField] private float _spawnRange = 3f;

    private void Update()
    {
        Vector3 position = _currentPosition;
        position.x += Random.Range(-_spawnRange, _spawnRange);

        TrySetGameObject(position);

        TryGetNewGameObject();
    }
}
