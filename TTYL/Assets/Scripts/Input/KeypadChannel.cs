using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "KeypadChannel", menuName = "ScriptableObjects/Input/KeypadChannel")]
public class KeypadChannel : ScriptableObject
{
    public UnityEvent KeypadButtonPressed { get; private set; } = new UnityEvent();
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
    public UnityEvent UpButtonPressed { get; private set; } = new UnityEvent();
    public UnityEvent UpButtonDown { get; private set; } = new UnityEvent();
    public UnityEvent UpButtonUp { get; private set; } = new UnityEvent();
    public UnityEvent DownButtonPressed { get; private set; } = new UnityEvent();
    public UnityEvent DownButtonDown { get; private set; } = new UnityEvent();
    public UnityEvent DownButtonUp { get; private set; } = new UnityEvent();
    public UnityEvent LeftButtonPressed { get; private set; } = new UnityEvent();
    public UnityEvent RightButtonPressed { get; private set; } = new UnityEvent();
    public UnityEvent ConfirmButtonPressed { get; private set; } = new UnityEvent();
    public UnityEvent LeftSelectionButtonPressed { get; private set; } = new UnityEvent();
    public UnityEvent RightSelectionButtonPressed { get; private set; } = new UnityEvent();
    public UnityEvent PickupButtonPressed { get; private set; } = new UnityEvent();
    public UnityEvent HangUpButtonPressed { get; private set; } = new UnityEvent();
    public UnityEvent AsteriskButtonPressed { get; private set; } = new UnityEvent();
    public UnityEvent<bool> GamePaused { get; private set; } = new UnityEvent<bool>();
}
