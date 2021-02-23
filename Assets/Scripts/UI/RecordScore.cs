using TMPro;
using UnityEngine;

public class RecordScore : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _tmpScore;

    private int _recordScore;

    private void OnEnable()
    {
        _player.Dying += OnPlayerDying;
    }

    private void OnDisable()
    {
        _player.Dying -= OnPlayerDying;
    }

    private void Start()
    {
        _recordScore = PlayerPrefs.GetInt("RecordScore", 0);
    }

    private void OnPlayerDying(int score)
    {
        _tmpScore.text = score.ToString() + " / " + _recordScore.ToString();

        if (_recordScore < score)
            PlayerPrefs.SetInt("RecordScore", score);
    }
}
