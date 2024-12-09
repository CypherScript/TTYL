using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class RapidClicker : MonoBehaviour
{
    [SerializeField] private float _clicksPerSecond;
    [SerializeField] private Button[] _buttonsToClick;
    private Coroutine _clickCoroutine;
    
    [Button]
    private void StartClick()
    {
        if (_clickCoroutine != null)
        {
            StopCoroutine(_clickCoroutine);
        }

        _clickCoroutine = StartCoroutine(RapidClick());
    }

    [Button]
    private void StopClick()
    {
        if (_clickCoroutine != null)
        {
            StopCoroutine(_clickCoroutine);
        }

        _clickCoroutine = null;
    }

    private IEnumerator RapidClick()
    {
        var secondsPerClick = 1 / _clicksPerSecond;

        while (true)
        {
            foreach (var button in _buttonsToClick)
            {
                button.onClick.Invoke();
            }
            yield return new WaitForSeconds(secondsPerClick);
        }
    }
}
