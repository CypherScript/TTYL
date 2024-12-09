using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNameMessageTyping : PhoneMessageTyping
{
    [SerializeField] private NameSetter _nameSetter;
    private bool _hasSetName;

    public override void OnConfirmPressed()
    {
        base.OnConfirmPressed();
        
        if (string.IsNullOrEmpty(InputField.Text))
        {
            return;
        }

        if (_hasSetName)
        {
            return;
        }

        _hasSetName = true;
        _nameSetter.SetName(InputField.Text);
    }
}
