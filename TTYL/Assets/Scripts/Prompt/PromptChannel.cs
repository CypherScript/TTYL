using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "PromptChannel", menuName = "ScriptableObjects/Prompt/Channel")]
public class PromptChannel : ScriptableObject
{
    [SerializeField] private string _text;

    public string Text
    {
        get => _text;
        set
        {
            _text = value;
            promptTextChanged?.Invoke();
        }
    }

    [ShowInInspector] public bool IsPromptActive { get; private set; }
    
    public bool IsTextFieldActive;
    public Guid PromptGuid { get; private set; } = Guid.Empty;
    
    public UnityEvent promptStarted = new UnityEvent();
    public UnityEvent<PromptResult> promptEnded = new UnityEvent<PromptResult>();
    public UnityEvent promptTextChanged = new UnityEvent();
    public UnityEvent<string> responseSubmitted = new UnityEvent<string>();

    public void StartPrompt()
    {
        IsPromptActive = true;
        PromptGuid = Guid.NewGuid();
        promptStarted?.Invoke();
    }

    public void EndPrompt(PromptResult result)
    {
        IsPromptActive = false;
        PromptGuid = Guid.Empty;
        promptEnded?.Invoke(result);
    }
    
    public void SubmitResponse(string response)
    {
        if (!IsPromptActive)
        {
            return;
        }
        responseSubmitted?.Invoke(response);
        ClearPrompt();
    }
    
    public void ClearPrompt()
    {
        Text = "";
        IsPromptActive = false;
    }
}

public enum PromptResult
{
    GoodReply = 0,
    BadReply = 1,
    NoReply = 2
}
