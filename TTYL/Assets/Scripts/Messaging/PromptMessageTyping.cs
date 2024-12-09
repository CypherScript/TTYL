using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PromptMessageTyping : PhoneMessageTyping
{
    [SerializeField] private PromptChannel _promptChannel;
    private Guid _promptGuid;

    protected override void OnEnable()
    {
        base.OnEnable();
        
        if (_promptChannel.PromptGuid == Guid.Empty || _promptChannel.PromptGuid != _promptGuid)
        {
            Clear();
            _promptGuid = _promptChannel.PromptGuid;
        }
        
        _promptChannel.promptStarted.AddListener(OnPromptStarted);
        _promptChannel.promptEnded.AddListener(OnPromptEnded);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _promptChannel.promptStarted.RemoveListener(OnPromptStarted);
        _promptChannel.promptEnded.RemoveListener(OnPromptEnded);
    }

    private void OnPromptStarted()
    {
        _promptGuid = _promptChannel.PromptGuid;
    }

    private void OnPromptEnded(PromptResult result)
    {
        Clear();
    }
    
    public override void OnConfirmPressed()
    {
        base.OnConfirmPressed();
        if (!gameObject.activeInHierarchy)
        {
            return;
        }
        
        if (!_promptChannel.IsPromptActive)
        {
            return;
        }

        if (string.IsNullOrEmpty(InputField.Text))
        {
            return;
        }
        
        _promptChannel.SubmitResponse(InputField.Text);
        Clear();
    }
}
