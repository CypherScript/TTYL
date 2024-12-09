using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopularityDisplay : MonoBehaviour
{
    [SerializeField] private FloatReference _popularity = null;
    [SerializeField] private GameObject _smallSignalBar = null;
    [SerializeField] private GameObject _mediumSignalBar = null;
    [SerializeField] private GameObject _largeSignalBar = null;
    [SerializeField] private CanvasGroup _flickerGroup = null;
    [SerializeField] private float _flickerDelay = 0.1f;

    private bool _isFlickering = false;
    private Coroutine _disableSignalBars = null;

    private void OnEnable()
    {
        _popularity.ValueChanged.AddListener(OnPopularityValueChanged);
        _flickerGroup.alpha = 1;
        ManageSignalBars();
    }

    private void OnDisable()
    {
        _popularity.ValueChanged.RemoveListener(OnPopularityValueChanged);
        if (_disableSignalBars != null) StopCoroutine(_disableSignalBars);
    }

    private void OnPopularityValueChanged(float value)
    {
        switch(_popularity.Value)
        {
            case 0:
                if (_disableSignalBars != null) StopCoroutine(_disableSignalBars);
                StartCoroutine(DisableSignalBar(_smallSignalBar));
                VibratePhone();
                break;
            case 1:
                if (_disableSignalBars != null) StopCoroutine(_disableSignalBars);
                StartCoroutine(DisableSignalBar(_mediumSignalBar));
                VibratePhone();
                break;
            case 2:
                if (_disableSignalBars != null) StopCoroutine(_disableSignalBars);
                StartCoroutine(DisableSignalBar(_largeSignalBar));
                VibratePhone();
                break;
            }
        }

    private void ManageSignalBars()
    {
        switch(_popularity.Value)
        {
            case 1:
                _smallSignalBar.SetActive(true);
                _mediumSignalBar.SetActive(false);
                _largeSignalBar.SetActive(false);
                break;
            case 2:
                _smallSignalBar.SetActive(true);
                _mediumSignalBar.SetActive(true);
                _largeSignalBar.SetActive(false);
                break;
            case 3:
                _smallSignalBar.SetActive(true);
                _mediumSignalBar.SetActive(true);
                _largeSignalBar.SetActive(true);
                break;
        }
    }

    private void VibratePhone()
    {
        #if !PLATFORM_WEBGL
                Handheld.Vibrate();
        #endif
    }

    private IEnumerator DisableSignalBar(GameObject go)
    {
        _isFlickering = true;
        float t = 0;

        while (_isFlickering)
        {
            t += Time.deltaTime;

            yield return new WaitForSeconds(_flickerDelay);
            _flickerGroup.alpha = 0;
            yield return new WaitForSeconds(_flickerDelay);
            _flickerGroup.alpha = 1;

            if (t >= 0.05f)
            {
                _isFlickering = false;
                _flickerGroup.alpha = 1;
            }
        }

        RectTransform rect = go.GetComponent<RectTransform>();
        Image image = go.GetComponent<Image>();

        if (image == null || rect == null) yield break;

        Color newColor = image.color;
        newColor.a = 1;

        Vector2 newSize = rect.sizeDelta;
        Vector2 originalSize = rect.sizeDelta;

        t = 0;

        while(t < 5f)
        {
            t += Time.deltaTime;
            newColor.a = Mathf.Lerp(newColor.a, 0, t / 5);
            image.color = newColor;

            newSize.y = Mathf.Lerp(newSize.y, 400, t / 10);
            rect.sizeDelta = newSize;
            yield return null;
        }

        go.SetActive(false);
        rect.sizeDelta = originalSize;
        newColor.a = 1;
        image.color = newColor;
    }
}
