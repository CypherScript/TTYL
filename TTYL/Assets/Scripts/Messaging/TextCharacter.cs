using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TextCharacter : MonoBehaviour
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextCharacterSettings _settings;
    
    public string Text
    {
        get => _text.text;
        set
        {
            _text.text = value;
            SetSelected();
        }
    }

    public void SetSelected()
    {
        _backgroundImage.color = _settings.SelectedColor;
        _text.color = _settings.SelectedTextColor;
    }

    public void SetUnSelected()
    {
        _backgroundImage.color = _settings.UnselectedColor;
        _text.color = _settings.UnselectedTextColor;
    }
}
