using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [SerializeField] private GameObject _target;

    private float _offset;

    private void Start()
    {
        _offset = transform.position.z - _target.transform.position.z;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, _target.transform.position.z + _offset);
    }
}
