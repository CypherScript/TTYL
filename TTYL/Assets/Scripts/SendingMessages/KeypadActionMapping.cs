using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class KeypadActionMapping : MonoBehaviour
{
    [SerializeField] private InputActionMappingChannel _inputActionMappingChannel;

    public void OnKeyPressed0()
    {
        _inputActionMappingChannel.Keypad0Pressed?.Invoke();
    }
    
    public void OnKeyPressed1()
    {
        _inputActionMappingChannel.Keypad1Pressed?.Invoke();
    }
    
    public void OnKeyPressed2()
    {
        _inputActionMappingChannel.Keypad2Pressed?.Invoke();
    }
    
    public void OnKeyPressed3()
    {
        _inputActionMappingChannel.Keypad3Pressed?.Invoke();
    }
    
    public void OnKeyPressed4()
    {
        _inputActionMappingChannel.Keypad4Pressed?.Invoke();
    }
    
    public void OnKeyPressed5()
    {
        _inputActionMappingChannel.Keypad5Pressed?.Invoke();
    }
    
    public void OnKeyPressed6()
    {
        _inputActionMappingChannel.Keypad6Pressed?.Invoke();
    }
    
    public void OnKeyPressed7()
    {
        _inputActionMappingChannel.Keypad7Pressed?.Invoke();
    }
    
    public void OnKeyPressed8()
    {
        _inputActionMappingChannel.Keypad8Pressed?.Invoke();
    }
    
    public void OnKeyPressed9()
    {
        _inputActionMappingChannel.Keypad9Pressed?.Invoke();
    }

    public void OnKeyPressedBackspace()
    {
        _inputActionMappingChannel.BackspacePressed?.Invoke();
    }

    public void OnConfirmPressed()
    {
        _inputActionMappingChannel.ConfirmButtonPressed?.Invoke();
    }

    public void OnRightArrowPressed()
    {
        _inputActionMappingChannel.RightButtonPressed?.Invoke();
    }
}
