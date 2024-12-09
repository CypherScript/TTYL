using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypingTutorialModule : TutorialModule
{
    [SerializeField] private string _promptString;
    [SerializeField] private PromptChannel _promptChannel;
    [SerializeField] private PromptTextDisplay _promptTextDisplay;
    [SerializeField] private PhoneTextField _textField;

    protected override void Start()
    {
        base.Start();
        _promptChannel.Text = _promptString;
    }

    protected override void OnModuleStarted()
    {
        base.OnModuleStarted();
        _textField.gameObject.SetActive(true);
        _promptTextDisplay.gameObject.SetActive(true);
    }
}
