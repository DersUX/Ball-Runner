using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _tmpScore;
    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private GameObject _restartPanel;

    private int _recordScore;

    private void Start()
    {
        _recordScore = PlayerPrefs.GetInt("RecordScore", 0);
    }

    private void OnEnable()
    {
        _player.Dying += OnPlayerDying;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        _player.Dying -= OnPlayerDying;
    }

    private void OnPlayerDying()
    {
        _gamePanel.SetActive(false);
        _restartPanel.SetActive(true);

        Time.timeScale = 0;

        _tmpScore.text = _player.Score.ToString() + " / " + _recordScore.ToString();

        if (_recordScore < _player.Score)
            PlayerPrefs.SetInt("RecordScore", _player.Score);
    }

    public void OnComebackButtonClick()
    {
        _gamePanel.SetActive(true);
        _restartPanel.SetActive(false);

        _player.GetComeback();
    }

    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene(1);
    }

    public void OnExitButtonClick()
    {
        SceneManager.LoadScene(0);
    }
}
