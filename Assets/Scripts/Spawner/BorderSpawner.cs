using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderSpawner : Spawner
{
    private void Start()
    {
        for (int i = 0; TryGetObject(out GameObject gameObject); i++)
            SetGameObject(gameObject, _currentPosition);
    }

    protected void Update()
    {
        TrySetGameObject(_currentPosition);

        TryGetNewGameObject();
    }
}
