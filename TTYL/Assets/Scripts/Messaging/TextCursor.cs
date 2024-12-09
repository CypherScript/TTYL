using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextCursor : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private float _timeBetweenBlinks;
    [SerializeField] private TextCharacterSettings _settings;

    private bool _isImageVisible = true;
    private Coroutine _blinkCoroutine;

    private void OnEnable()
    {
        SetActive(true);
    }

    private void OnDisable()
    {
        SetActive(false);
    }

    public void SetActive(bool isActive)
    {
        if (isActive)
        {
            _image.color = _settings.TextCursorColor;
            _isImageVisible = true;
            _blinkCoroutine = StartCoroutine(Blink());
        }
        else
        {
            StopCoroutine(_blinkCoroutine);
            _isImageVisible = false;
            _image.color = new Color(0, 0, 0, 0);
        }
    }

    private IEnumerator Blink()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeBetweenBlinks);
            
            if (_isImageVisible)
            {
                _isImageVisible = false;
                _image.enabled = false;
            }
            else
            {
                _isImageVisible = true;
                _image.enabled = true;
            }
        }
    }
}
