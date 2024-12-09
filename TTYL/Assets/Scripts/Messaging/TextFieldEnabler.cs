using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFieldEnabler : MonoBehaviour
{
    [SerializeField] private PromptChannel _promptChannel;
    [SerializeField] private PhoneMessageDisplayer _phoneMessageDisplayer;
    [SerializeField] private GameObject _TextFieldGameObject;
    [SerializeField] private float _textFieldDelay = 4f;

    private Coroutine _delayTextField = null;

    private void Awake()
    {
        _promptChannel.IsTextFieldActive = false;
        _TextFieldGameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _promptChannel.promptStarted.AddListener(OnPromptStarted);
        _promptChannel.promptEnded.AddListener(OnPromptEnded);

        if(_promptChannel.IsPromptActive && !_promptChannel.IsTextFieldActive)
        {
            // if (_delayTextField != null)
            // {
            //     StopCoroutine(_delayTextField);
            // }
            //
            // _delayTextField = StartCoroutine(DelayTextField());
            
            _TextFieldGameObject.SetActive(true);
            _promptChannel.IsTextFieldActive = true;
        }
    }

    private void OnDisable()
    {
        _promptChannel.promptStarted.RemoveListener(OnPromptStarted);
        _promptChannel.promptEnded.RemoveListener(OnPromptEnded);

        // if (_delayTextField != null)
        // {
        //     StopCoroutine(_delayTextField);
        // }
    }

    private void OnPromptStarted()
    {
        // if (_delayTextField != null)
        // {
        //     StopCoroutine(_delayTextField);
        // }
        //
        // _delayTextField = StartCoroutine(DelayTextField());
        
        _TextFieldGameObject.SetActive(true);
        _promptChannel.IsTextFieldActive = true;
    }

    private void OnPromptEnded(PromptResult result)
    {
        // if (_delayTextField != null)
        // {
        //     StopCoroutine(_delayTextField);
        // }
        _TextFieldGameObject.SetActive(false);
        _promptChannel.IsTextFieldActive = false;
    }

    // private IEnumerator DelayTextField()
    // {
    //     _phoneMessageDisplayer.DisplayPlayerTypingBubble(true);
    //
    //     yield return new WaitForSeconds(_textFieldDelay);
    //
    //     _phoneMessageDisplayer.DisplayPlayerTypingBubble(false);
    //     _TextFieldGameObject.SetActive(true);
    //     _promptChannel.IsTextFieldActive = true;
    // }
}
