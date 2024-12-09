using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Yarn.Unity;

public class NameSetter : MonoBehaviour
{
    [SerializeField] private DialogueRunner _dialogueRunner;
    [SerializeField] private bool _shouldSetNameOnAwake;
    [SerializeField] private bool _shouldClearNameOnAwake;
    [SerializeField] private StringReference _playerName;

    private void Awake()
    {
        if (_shouldClearNameOnAwake)
        {
            _playerName.Value = "";
        }
        if (!_shouldSetNameOnAwake)
        {
            return;
        }
        
        SetName();
    }

    [Button]
    public void SetName()
    {
        _dialogueRunner.VariableStorage.SetValue("$playerName", _playerName.Value);
    }
    
    public void SetName(string playerName)
    {
        _playerName.Value = playerName;
        SetName();
    }
}
