using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Yarn.Unity;

public class TypingBubble : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private float _lettersPerSecond;
    [SerializeField] private bool _shouldLoop;

    private Coroutine _typewriterCoroutine;
    
    private void OnEnable()
    {
        if (_typewriterCoroutine != null)
        {
            StopCoroutine(_typewriterCoroutine);
        }
        _typewriterCoroutine = StartCoroutine(Typewriter(_shouldLoop));
    }

    private void OnDisable()
    {
        if (_typewriterCoroutine != null)
        {
            StopCoroutine(_typewriterCoroutine);
        }
    }

    private IEnumerator Typewriter(bool shouldLoop)
    {
        do
        {
            _text.maxVisibleCharacters = 0;
            yield return null;

            var characterCount = _text.textInfo.characterCount;

            if (_lettersPerSecond <= 0 || characterCount == 0)
            {
                _text.maxVisibleCharacters = characterCount;
                yield break;
            }

            // cache letters per second reciprocal
            float secondsPerLetter = 1.0f / _lettersPerSecond;

            var elapsedTime = Time.deltaTime;

            while (_text.maxVisibleCharacters < characterCount)
            {
                while (elapsedTime >= secondsPerLetter)
                {
                    _text.maxVisibleCharacters += 1;
                    elapsedTime -= secondsPerLetter;
                }

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            _text.maxVisibleCharacters = characterCount;
            yield return new WaitForSeconds(secondsPerLetter);
            
        } while (shouldLoop);
    }
}
