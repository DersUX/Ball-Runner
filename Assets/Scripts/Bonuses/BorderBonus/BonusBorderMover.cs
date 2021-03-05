using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BonusBorderMover : MonoBehaviour
{
    [SerializeField] private BorderBonus _bonus;
    [SerializeField] private float _durationTime = 30f;

    private List<GameObject> _borders;

    private Player _player;

    private bool _isActive = false;
    private float _currentTime;
    private float _offset;

    private void OnEnable()
    {
        _currentTime = 0;

        _bonus.Activate += BonusActivate;
    }

    private void OnDisable()
    {
        _bonus.Activate -= BonusActivate;
    }

    private void BonusActivate(List<GameObject> borders, Player player)
    {
        _borders = borders;
        _player = player;
        _offset = transform.position.z - _player.transform.position.z;

        _isActive = true;
    }

    private void LateUpdate()
    {
        if (_isActive && _durationTime >= _currentTime)
        {
            _currentTime += Time.deltaTime;

            foreach (var border in _borders)
                border.transform.position = new Vector3(border.transform.position.x, border.transform.position.y, _player.transform.position.z + _offset);
        }
        else if (_isActive)
        {
            _isActive = false;

            foreach (var border in _borders)
                border.transform.DOScale(new Vector3(0, 0, border.transform.localScale.z), 2).OnComplete(onBorderDisabling);

        }
    }

    private void onBorderDisabling()
    {
        foreach (var border in _borders)
            Destroy(border.gameObject);

        gameObject.SetActive(false);
    }
}
