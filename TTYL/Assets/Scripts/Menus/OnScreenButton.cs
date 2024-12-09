using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnScreenButton : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _selectedSprite;
    [SerializeField] private Sprite _unSelectedSprite;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private OnScreenButtonSettings _settings;

    private void Awake()
    {
        SetUnSelected();
    }

    public void SetSelected()
    {
        _image.sprite = _selectedSprite;
        //_image.color = _settings.SelectedColor;
        _text.color = _settings.SelectedTextColor;
    }

    public void SetUnSelected()
    {
        _image.sprite = _unSelectedSprite;
        //_image.color = _settings.UnselectedColor;
        _text.color = _settings.UnselectedTextColor;
    }
}
