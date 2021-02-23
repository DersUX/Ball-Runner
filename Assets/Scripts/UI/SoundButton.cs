using System;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Sprite _imageOn;
    [SerializeField] private Sprite _imageOff;

    private Image _image;
    private bool _soundOn;

    private void Awake()
    {
        _soundOn = Convert.ToBoolean(PlayerPrefs.GetInt("Sound", 1));
    }

    private void Start()
    {
        _image = _button.GetComponent<Image>();
        SwitchImage();
    }

    public void OnButtonClick()
    {
        _soundOn = !_soundOn;
        PlayerPrefs.SetInt("Sound", Convert.ToInt32(_soundOn));

        SwitchImage();
    }
    
    private void SwitchImage()
    {
        if (!_soundOn)
            _image.sprite = _imageOff;
        else
            _image.sprite = _imageOn;
    }
}
