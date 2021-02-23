using System;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [SerializeField] GameObject _panel;

    private bool _gamePause = false;

    public void OnClickButton()
    {
        _gamePause = !_gamePause;
        Time.timeScale = Convert.ToInt32(!_gamePause);

        _panel.SetActive(_gamePause);
    }
}
