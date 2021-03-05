using System;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Sprite _imageOn;
    [SerializeField] private Sprite _imageOff;

    private Image _image;
    private bool _soundEnable;

    private void Awake()
    {
        _soundEnable = Convert.ToBoolean(PlayerPrefs.GetInt("Sound", 1));
    }

    private void Start()
    {
        _image = _button.GetComponent<Image>();
        SwitchImage();
    }

    public void OnSoundButtonClick()
    {
        _soundEnable = !_soundEnable;
        PlayerPrefs.SetInt("Sound", Convert.ToInt32(_soundEnable));

        SwitchImage();
    }
    
    private void SwitchImage()
    {
        if (!_soundEnable)
            _image.sprite = _imageOff;
        else
            _image.sprite = _imageOn;
    }
}
