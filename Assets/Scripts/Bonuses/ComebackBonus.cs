using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComebackBonus : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.AddHealth();
            gameObject.SetActive(false);
        }
    }
}
