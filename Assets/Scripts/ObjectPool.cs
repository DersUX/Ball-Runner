using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private int _capacity = 50;

    private List<GameObject> _pool = new List<GameObject>();

    protected int PoolCount => _pool.Count;

    protected void Initialize(List<GameObject> prefabList)
    {
        for (int i = 0; i < _capacity; i++)
            AddObjectToList(prefabList[Random.Range(0, prefabList.Count)]);
    }

    protected void AddObjectToList(GameObject prefab)
    {
        GameObject spawned = Instantiate(prefab, transform);

        spawned.transform.SetParent(_container.transform);
        spawned.SetActive(false);

        _pool.Add(spawned);
    }

    protected bool TryGetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault(p => p.activeSelf == false);

        return result != null;
    }
}
