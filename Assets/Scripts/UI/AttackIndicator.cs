using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AttackIndicator : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _indicator;
    [SerializeField] private GameObject _target;
    [SerializeField] private float _size = 100;
    [SerializeField] private float _alphaChangeDuration = 0.8f;

    private float _border;
    private float _halfScreenHeight;
    private float _halfScreenWidth;

    private Image _image;
    private Tweener _tweener;

    private void Start()
    {
        _halfScreenHeight = Screen.height / 2f;
        _halfScreenWidth = Screen.width / 2f;
        _border = _size / 2f;

        _image = _indicator.GetComponent<Image>();

        _tweener = DOTween.ToAlpha(() => _image.color, x => _image.color = x, 0.6f, _alphaChangeDuration).SetLoops(-1, LoopType.Yoyo).Pause();
    }

    private void Update()
    {
        if (_indicator.activeSelf)
        {
            var screenPoint = Camera.main.WorldToScreenPoint(_target.transform.localPosition);
            float angle = Mathf.Atan2(screenPoint.y - _halfScreenHeight, screenPoint.x - _halfScreenWidth);
            float y = -_halfScreenHeight + _border + 350f;
            float x = y / Mathf.Tan(angle);

            _indicator.transform.localPosition = new Vector3(x, y, 0);
            _indicator.transform.localEulerAngles = new Vector3(0, 0, angle * Mathf.Rad2Deg - 90);
        }
    }

    private void OnEnable()
    {
        _player.EnemyAlerting += OnVisibleChanged;
        
    }

    private void OnDisable()
    {
        _player.EnemyAlerting -= OnVisibleChanged;
    }

    private void OnVisibleChanged(bool isVisible)
    {
        _indicator.SetActive(isVisible);

        if (isVisible)
            _tweener.Play();
        else
            _tweener.Pause();
    }
}