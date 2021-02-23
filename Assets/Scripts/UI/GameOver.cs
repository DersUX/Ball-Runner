using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private GameObject _restartPanel;

    private void OnEnable()
    {
        _player.Dying += OnPlayerDying;
    }

    private void OnDisable()
    {
        _player.Dying -= OnPlayerDying;
    }

    private void OnPlayerDying(int score)
    {
        _gamePanel.SetActive(false);
        _restartPanel.SetActive(true);

        Time.timeScale = 0;
    }

    public void OnRestartButtonClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void OnExitButtonClick()
    {
        SceneManager.LoadScene(0);
    }
}
