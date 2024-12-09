using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleCard : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI[] _texts;
    [SerializeField] private Image[] _images;
    
    public void FadeOut(float duration, Action onFadeComplete, bool shouldDestroy = true)
    {
        if (_texts.Length > 0)
        {
            foreach (TextMeshProUGUI text in _texts)
            {
                text.DOFade(0, duration);
            }
        }

        if (_images.Length > 0)
        {
            foreach (Image image in _images)
            {
                image.DOFade(0, duration);
            }
        }

        _image.DOFade(0, duration)
            .OnComplete(() =>
            {
                onFadeComplete?.Invoke();

                if (shouldDestroy)
                {
                    Destroy(_image.gameObject);
                }
            });
    }

    public void FadeIn(float duration, Action onFadeComplete)
    {
        _image.DOFade(1, duration)
            .OnComplete(() =>
            {
                onFadeComplete?.Invoke();
            });
    }

    public void FadeInText(float duration, int index, Action onFadeComplete)
    {
        if (index < 0)
        {
            return;
        }

        if (index > _texts.Length - 1)
        {
            return;
        }
        
        _texts[index].DOFade(1, duration).OnComplete(() => { onFadeComplete?.Invoke(); });
    }

    public void SnapInText(int index, Action onSnapComplete)
    {
        if (index < 0)
        {
            return;
        }

        if (index > _texts.Length - 1)
        {
            return;
        }

        _texts[index].alpha = 1;
        onSnapComplete?.Invoke();
    }
}
