using UnityEngine;

public class JumpBonus : MonoBehaviour
{
    [SerializeField] private Vector3 _jumpDirection;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Player>(out Player player))
            other.gameObject.GetComponent<Rigidbody>().AddForce(_jumpDirection);
    }
}
