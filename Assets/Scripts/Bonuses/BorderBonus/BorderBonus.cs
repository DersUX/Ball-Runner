using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BorderBonus : MonoBehaviour
{
    [SerializeField] private GameObject _border;
    [SerializeField] private float _spawnDistance = 4;

    private List<GameObject> _borders = new List<GameObject>();

    public event UnityAction<List<GameObject>, Player> Activate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Player>(out Player player))
        {
            SpawnBorder(player.transform.position, -_spawnDistance);

            SpawnBorder(player.transform.position, _spawnDistance);

            Activate?.Invoke(_borders, player);

            gameObject.SetActive(false);
        }
    }

    private void SpawnBorder(Vector3 position, float positionMix)
    {
        Vector3 currentPosition = position;
        currentPosition.x = 0 + positionMix;

        GameObject spawned = Instantiate(_border, currentPosition, Quaternion.identity);

        _borders.Add(spawned);
    }
}
