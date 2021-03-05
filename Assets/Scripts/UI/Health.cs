using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private TMP_Text _tmpHealth;
    [SerializeField] private Player _player;

    private void Start()
    {
        OnHealthChanged(_player.Health);
    }

    private void OnEnable()
    {
        _player.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int health)
    {
        _tmpHealth.text = "x" + health.ToString();
    }
}
