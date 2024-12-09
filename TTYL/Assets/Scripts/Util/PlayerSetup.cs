using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Yarn.Unity;

public class PlayerSetup : MonoBehaviour
{
    [SerializeField] private DialogueRunner _dialogueRunner;
    [SerializeField] private GameObject _groupChat;

    private Coroutine _setPlayerNameCoroutine;
    private Effects.CoroutineInterruptToken _coroutineInterruptToken;
    private bool _isPlayerNameSubmitted;
    private string _playerName;
    
    public void SubmitName(string playerName)
    {
        _playerName = playerName;
        
        if (_coroutineInterruptToken.CanInterrupt)
        {
            _isPlayerNameSubmitted = true;
        }
    }

    [YarnCommand("SetPlayerName")]
    public Coroutine SetPlayerName()
    {
        if (_coroutineInterruptToken is { CanInterrupt: true })
        {
            _coroutineInterruptToken.Interrupt();
        }
        _coroutineInterruptToken = new Effects.CoroutineInterruptToken();
        
        _setPlayerNameCoroutine = StartCoroutine(WaitForPlayerName(_coroutineInterruptToken));
        return _setPlayerNameCoroutine;
    }

    private IEnumerator WaitForPlayerName(Effects.CoroutineInterruptToken interruptToken)
    {
        interruptToken.Start();
        while (!_isPlayerNameSubmitted)
        {
            yield return null;
        }
        
        _dialogueRunner.VariableStorage.SetValue("$playerName", _playerName);
        _groupChat.SetActive(true);
        interruptToken.Complete();
        Destroy(gameObject);
    }
}
