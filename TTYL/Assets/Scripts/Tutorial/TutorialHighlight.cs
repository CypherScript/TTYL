using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

public class TutorialHighlight : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TutorialHighlightSettings _settings;

    private TweenerCore<Color, Color, ColorOptions> _fadeTween;
    private float _flashingTimer;
    private bool _isFlashing;

    private void Awake()
    {
        _image.color = _settings.Color;
    }

    public void OnFlash()
    {
        Debug.Log($"{gameObject} OnFlash");
        _flashingTimer = 0;
        _isFlashing = true;
        StartPulse();
    }

    private void OnDisable()
    {
        StopPulse();
    }

    private void Update()
    {
        if (!_isFlashing)
        {
            return;
        }

        _flashingTimer += Time.deltaTime;
        if (_flashingTimer >= _settings.RevealTime)
        {
            _isFlashing = false;
            _flashingTimer = 0;
            StopPulse();
        }
    }

    public void StartPulse()
    {
        _image.color = _settings.Color;
        FadeIn();
    }

    public void StopPulse()
    {
        _image.color = _settings.Color;
        _fadeTween?.Kill();
    }

    private void FadeIn()
    {
        _fadeTween?.Kill();
        _fadeTween = _image.DOFade(_settings.MaxAlpha, _settings.PulseTime).OnComplete(FadeOut);
    }

    private void FadeOut()
    {
        _fadeTween?.Kill();
        _fadeTween = _image.DOFade(_settings.MinAlpha, _settings.PulseTime).OnComplete(FadeIn);
    }
}
