using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "InputActionMappingChannel", menuName = "ScriptableObjects/Input/InputActionMappingChannel")]
public class InputActionMappingChannel : ScriptableObject
{
    public UnityEvent Keypad0Pressed { get; private set; } = new UnityEvent();
    public UnityEvent Keypad1Pressed { get; private set; } = new UnityEvent();
    public UnityEvent Keypad2Pressed { get; private set; } = new UnityEvent();
    public UnityEvent Keypad3Pressed { get; private set; } = new UnityEvent();
    public UnityEvent Keypad4Pressed { get; private set; } = new UnityEvent();
    public UnityEvent Keypad5Pressed { get; private set; } = new UnityEvent();
    public UnityEvent Keypad6Pressed { get; private set; } = new UnityEvent();
    public UnityEvent Keypad7Pressed { get; private set; } = new UnityEvent();
    public UnityEvent Keypad8Pressed { get; private set; } = new UnityEvent();
    public UnityEvent Keypad9Pressed { get; private set; } = new UnityEvent();
    public UnityEvent BackspacePressed { get; private set; } = new UnityEvent();
    public UnityEvent RightButtonPressed { get; private set; } = new UnityEvent();
    public UnityEvent ConfirmButtonPressed { get; private set; } = new UnityEvent();

    private void OnDestroy()
    {
        Keypad0Pressed.RemoveAllListeners();
        Keypad1Pressed.RemoveAllListeners();
        Keypad2Pressed.RemoveAllListeners();
        Keypad3Pressed.RemoveAllListeners();
        Keypad4Pressed.RemoveAllListeners();
        Keypad5Pressed.RemoveAllListeners();
        Keypad6Pressed.RemoveAllListeners();
        Keypad7Pressed.RemoveAllListeners();
        Keypad8Pressed.RemoveAllListeners();
        Keypad9Pressed.RemoveAllListeners();
        BackspacePressed.RemoveAllListeners();
        ConfirmButtonPressed.RemoveAllListeners();
        RightButtonPressed.RemoveAllListeners();
    }
}
