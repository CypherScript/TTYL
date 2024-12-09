using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageDisplay : MonoBehaviour
{
    [SerializeField] private KeypadChannel _keypadChannel;
    [SerializeField] private MessageDisplayChannel _messageDisplayChannel;
    [SerializeField] private GameObject _pauseGO;

    private void Awake()
    {
        _pauseGO.SetActive(false);
    }

    private void OnEnable()
    {
        _keypadChannel.LeftSelectionButtonPressed.AddListener(OnLeftSelectionButtonPressed);
        _keypadChannel.GamePaused.AddListener(OnGamePaused);
    }

    private void OnDisable()
    {
        _keypadChannel.LeftSelectionButtonPressed.RemoveListener(OnLeftSelectionButtonPressed);
        _keypadChannel.GamePaused.RemoveListener(OnGamePaused);
    }

    private void OnLeftSelectionButtonPressed()
    {
        _messageDisplayChannel.BackButtonPressed?.Invoke();
    }

    private void OnGamePaused(bool isPaused)
    {
        if (isPaused)
            _pauseGO.SetActive(true);
        else
            _pauseGO.SetActive(false);
    }
}
