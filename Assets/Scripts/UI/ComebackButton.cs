using UnityEngine;
using UnityEngine.UI;

public class ComebackButton : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.interactable = _player.Health > 0;
    }
}
