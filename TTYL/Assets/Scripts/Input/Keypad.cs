using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{
    [SerializeField] protected KeypadChannel KeypadChannel;
    [SerializeField] protected PhoneCallChannel PhoneCallChannel;
    
    [BoxGroup("Keyboard Buttons")] [SerializeField]
    private Button
        _button0,
        _button1,
        _button2,
        _button3,
        _button4,
        _button5,
        _button6,
        _button7,
        _button8,
        _button9,
        _buttonConfirm,
        _buttonUp,
        _buttonDown,
        _buttonLeft,
        _buttonRight,
        _buttonBackspace,
        _buttonLeftSelect,
        _buttonRightSelect,
        _buttonPickup,
        _buttonHangup,
        _buttonAsterisk;

    [SerializeField] private EventTrigger _downButtonEventTrigger;
    
    protected virtual void OnEnable()
    {
        RegisterGuiCallbacks();
        PhoneCallChannel.PhoneCall.AddListener(UnregisterButtonsForPhoneCall);
        PhoneCallChannel.PhoneCallAnswered.AddListener(UnregisterButtonsWhenAnswering);
        PhoneCallChannel.PhoneCallEnded.AddListener(RegisterButtonsForPhoneCall);
    }

    protected virtual void OnDisable()
    {
        UnregisterGuiCallbacks();
        PhoneCallChannel.PhoneCall.RemoveListener(UnregisterButtonsForPhoneCall);
        PhoneCallChannel.PhoneCallAnswered.RemoveListener(UnregisterButtonsWhenAnswering);
        PhoneCallChannel.PhoneCallEnded.RemoveListener(RegisterButtonsForPhoneCall);
    }

    private void UnregisterButtonsWhenAnswering(Characters character)
    {
        _buttonPickup.interactable = false;
        _buttonHangup.interactable = false;
    }
    
    private void RegisterGuiCallbacks()
    {
        _button0.onClick.AddListener(OnKeyPressed0);
        _button1.onClick.AddListener(OnKeyPressed1);
        _button2.onClick.AddListener(OnKeyPressed2);
        _button3.onClick.AddListener(OnKeyPressed3);
        _button4.onClick.AddListener(OnKeyPressed4);
        _button5.onClick.AddListener(OnKeyPressed5);
        _button6.onClick.AddListener(OnKeyPressed6);
        _button7.onClick.AddListener(OnKeyPressed7);
        _button8.onClick.AddListener(OnKeyPressed8);
        _button9.onClick.AddListener(OnKeyPressed9);
        _buttonConfirm.onClick.AddListener(OnConfirmPressed);
        _buttonBackspace.onClick.AddListener(OnKeyPressedBackspace);
        _buttonUp.onClick.AddListener(OnUpArrowPressed);
        _buttonDown.onClick.AddListener(OnDownArrowPressed);
        _buttonLeft.onClick.AddListener(OnLeftArrowPressed);
        _buttonRight.onClick.AddListener(OnRightArrowPressed);
        _buttonLeftSelect.onClick.AddListener(OnLeftSelectionKeyPressed);
        _buttonRightSelect.onClick.AddListener(OnRightSelectionKeyPressed);
        _buttonPickup.onClick.AddListener(OnPickupKeyPressed);
        _buttonHangup.onClick.AddListener(OnHangupKeyPressed);
        _buttonAsterisk.onClick.AddListener(OnAsteriskKeyPressed);
    }
    
    private void UnregisterGuiCallbacks()
    {
        _button0.onClick.RemoveListener(OnKeyPressed0);
        _button1.onClick.RemoveListener(OnKeyPressed1);
        _button2.onClick.RemoveListener(OnKeyPressed2);
        _button3.onClick.RemoveListener(OnKeyPressed3);
        _button4.onClick.RemoveListener(OnKeyPressed4);
        _button5.onClick.RemoveListener(OnKeyPressed5);
        _button6.onClick.RemoveListener(OnKeyPressed6);
        _button7.onClick.RemoveListener(OnKeyPressed7);
        _button8.onClick.RemoveListener(OnKeyPressed8);
        _button9.onClick.RemoveListener(OnKeyPressed9);
        _buttonConfirm.onClick.RemoveListener(OnConfirmPressed);
        _buttonBackspace.onClick.RemoveListener(OnKeyPressedBackspace);
        _buttonUp.onClick.RemoveListener(OnUpArrowPressed);
        _buttonDown.onClick.RemoveListener(OnDownArrowPressed);
        _buttonLeft.onClick.RemoveListener(OnLeftArrowPressed);
        _buttonRight.onClick.RemoveListener(OnRightArrowPressed);
        _buttonLeftSelect.onClick.RemoveListener(OnLeftSelectionKeyPressed);
        _buttonRightSelect.onClick.RemoveListener(OnRightSelectionKeyPressed);
        _buttonPickup.onClick.RemoveListener(OnPickupKeyPressed);
        _buttonHangup.onClick.RemoveListener(OnHangupKeyPressed);
        _buttonAsterisk.onClick.RemoveListener(OnAsteriskKeyPressed);
    }

    private void UnregisterButtonsForPhoneCall()
    {
        _button0.interactable = false;
        _button1.interactable = false;
        _button2.interactable = false;
        _button3.interactable = false;
        _button4.interactable = false;
        _button5.interactable = false;
        _button6.interactable = false;
        _button7.interactable = false;
        _button8.interactable = false;
        _button9.interactable = false;
        _buttonConfirm.interactable = false;
        _buttonUp.interactable = false;
        _buttonDown.interactable = false;
        _buttonLeft.interactable = false;
        _buttonRight.interactable = false;
        _buttonLeftSelect.interactable = false;
        _buttonRightSelect.interactable = false;
        _buttonAsterisk.interactable = false;
    }

    private void RegisterButtonsForPhoneCall(bool wasAnswered)
    {
        _button0.interactable = true;
        _button1.interactable = true;
        _button2.interactable = true;
        _button3.interactable = true;
        _button4.interactable = true;
        _button5.interactable = true;
        _button6.interactable = true;
        _button7.interactable = true;
        _button8.interactable = true;
        _button9.interactable = true;
        _buttonConfirm.interactable = true;
        _buttonUp.interactable = true;
        _buttonDown.interactable = true;
        _buttonLeft.interactable = true;
        _buttonRight.interactable = true;
        _buttonLeftSelect.interactable = true;
        _buttonRightSelect.interactable = true;
        _buttonAsterisk.interactable = true;
        _buttonPickup.interactable = true;
        _buttonHangup.interactable = true;
    }

    private void UnregisterButtonsForPause()
    {
        _button0.interactable = false;
        _button1.interactable = false;
        _button2.interactable = false;
        _button3.interactable = false;
        _button4.interactable = false;
        _button5.interactable = false;
        _button6.interactable = false;
        _button7.interactable = false;
        _button8.interactable = false;
        _button9.interactable = false;
        _buttonConfirm.interactable = false;
        _buttonLeft.interactable = false;
        _buttonRight.interactable = false;
        _buttonLeftSelect.interactable = false;
        _buttonRightSelect.interactable = false;
        _buttonAsterisk.interactable = false;
        _buttonPickup.interactable = false;
        _buttonHangup.interactable = false;
    }

    private void RegisterButtonsForUnpause()
    {
        if (PhoneCallChannel.IsPhoneCallActive && !PhoneCallChannel.IsCallActive)
        {
            _buttonPickup.interactable = true;
            _buttonHangup.interactable = true;
        }
        else if(PhoneCallChannel.IsPhoneCallActive && PhoneCallChannel.IsCallActive)
        {

        }
        else
        {
            _button0.interactable = true;
            _button1.interactable = true;
            _button2.interactable = true;
            _button3.interactable = true;
            _button4.interactable = true;
            _button5.interactable = true;
            _button6.interactable = true;
            _button7.interactable = true;
            _button8.interactable = true;
            _button9.interactable = true;
            _buttonConfirm.interactable = true;
            _buttonLeft.interactable = true;
            _buttonRight.interactable = true;
            _buttonLeftSelect.interactable = true;
            _buttonRightSelect.interactable = true;
            _buttonAsterisk.interactable = true;
            _buttonPickup.interactable = true;
            _buttonHangup.interactable = true;
        }
    }

    private void PauseGame()
    {
        if (Time.timeScale == 1)
        {
            KeypadChannel.GamePaused?.Invoke(true);
            Time.timeScale = 0;
            UnregisterButtonsForPause();
            Debug.Log("GAME IS PAUSED");
        }
        else
        {
            KeypadChannel.GamePaused?.Invoke(false);
            Time.timeScale = 1;
            RegisterButtonsForUnpause();
            Debug.Log("GAME IS UNPAUSED");
        }
    }

    //Space key
    public virtual void OnKeyPressed0()
    {
        OnKeyPressed(PhoneKeypadButton.Key0);
    }
    
    public virtual void OnKeyPressed1()
    {
        OnKeyPressed(PhoneKeypadButton.Key1);
    }
    
    public virtual void OnKeyPressed2()
    {
        OnKeyPressed(PhoneKeypadButton.Key2);
    }
    
    public virtual void OnKeyPressed3()
    {
        OnKeyPressed(PhoneKeypadButton.Key3);
    }
    
    public virtual void OnKeyPressed4()
    {
        OnKeyPressed(PhoneKeypadButton.Key4);
    }
    
    public virtual void OnKeyPressed5()
    {
        OnKeyPressed(PhoneKeypadButton.Key5);
    }
    
    public virtual void OnKeyPressed6()
    {
        OnKeyPressed(PhoneKeypadButton.Key6);
    }
    
    public virtual void OnKeyPressed7()
    {
        OnKeyPressed(PhoneKeypadButton.Key7);
    }
    
    public virtual void OnKeyPressed8()
    {
        OnKeyPressed(PhoneKeypadButton.Key8);
    }
    
    public virtual void OnKeyPressed9()
    {
        OnKeyPressed(PhoneKeypadButton.Key9);
    }

    public virtual void OnKeyPressedBackspace()
    {
        OnKeyPressed(PhoneKeypadButton.Backspace);
        PauseGame();
    }
    
    public virtual void OnConfirmPressed()
    {
        OnKeyPressed(PhoneKeypadButton.Confirm);
    }
    
    public virtual void OnUpArrowPressed()
    {
        OnKeyPressed(PhoneKeypadButton.Up);
    }
    
    public void OnUpArrowDown()
    {
        KeypadChannel.UpButtonDown?.Invoke();
    }

    public void OnUpArrowUp()
    {
        KeypadChannel.UpButtonUp?.Invoke();
    }
    
    public virtual void OnDownArrowPressed()
    {
        OnKeyPressed(PhoneKeypadButton.Down);
    }

    public void OnDownArrowDown()
    {
        KeypadChannel.DownButtonDown?.Invoke();
    }

    public void OnDownArrowUp()
    {
        KeypadChannel.DownButtonUp?.Invoke();
    }
    
    public virtual void OnLeftArrowPressed()
    {
        OnKeyPressed(PhoneKeypadButton.Left);
    }
    
    public virtual void OnRightArrowPressed()
    {
        OnKeyPressed(PhoneKeypadButton.Right);
    }

    public virtual void OnLeftSelectionKeyPressed()
    {
        OnKeyPressed(PhoneKeypadButton.LeftSelection);
    }

    public virtual void OnRightSelectionKeyPressed()
    {
        OnKeyPressed(PhoneKeypadButton.RightSelection);
    }
    
    public virtual void OnAsteriskKeyPressed()
    {
        OnKeyPressed(PhoneKeypadButton.Asterisk);
    }
    
    public virtual void OnPickupKeyPressed()
    {
        OnKeyPressed(PhoneKeypadButton.Pickup);
    }
    
    public virtual void OnHangupKeyPressed()
    {
        OnKeyPressed(PhoneKeypadButton.Hangup);
    }
    
    public virtual void OnKeyPressed(PhoneKeypadButton pressedButton)
    {
        KeypadChannel.KeypadButtonPressed?.Invoke();

        switch (pressedButton)
        {
            case PhoneKeypadButton.Key0:
                KeypadChannel.Keypad0Pressed?.Invoke();
                break;
            case PhoneKeypadButton.Key1:
                KeypadChannel.Keypad1Pressed?.Invoke();
                break;
            case PhoneKeypadButton.Key2:
                KeypadChannel.Keypad2Pressed?.Invoke();
                break;
            case PhoneKeypadButton.Key3:
                KeypadChannel.Keypad3Pressed?.Invoke();
                break;
            case PhoneKeypadButton.Key4:
                KeypadChannel.Keypad4Pressed?.Invoke();
                break;
            case PhoneKeypadButton.Key5:
                KeypadChannel.Keypad5Pressed?.Invoke();
                break;
            case PhoneKeypadButton.Key6:
                KeypadChannel.Keypad6Pressed?.Invoke();
                break;
            case PhoneKeypadButton.Key7:
                KeypadChannel.Keypad7Pressed?.Invoke();
                break;
            case PhoneKeypadButton.Key8:
                KeypadChannel.Keypad8Pressed?.Invoke();
                break;
            case PhoneKeypadButton.Key9:
                KeypadChannel.Keypad9Pressed?.Invoke();
                break;
            case PhoneKeypadButton.Confirm:
                KeypadChannel.ConfirmButtonPressed?.Invoke();
                break;
            case PhoneKeypadButton.Up:
                KeypadChannel.UpButtonPressed?.Invoke();
                break;
            case PhoneKeypadButton.Down:
                KeypadChannel.DownButtonPressed?.Invoke();
                break;
            case PhoneKeypadButton.Left:
                KeypadChannel.LeftButtonPressed?.Invoke();
                break;
            case PhoneKeypadButton.Right:
                KeypadChannel.RightButtonPressed?.Invoke();
                break;
            case PhoneKeypadButton.LeftSelection:
                KeypadChannel.LeftSelectionButtonPressed?.Invoke();
                break;
            case PhoneKeypadButton.RightSelection:
                KeypadChannel.RightSelectionButtonPressed?.Invoke();
                break;
            case PhoneKeypadButton.Pickup:
                KeypadChannel.PickupButtonPressed?.Invoke();
                break;
            case PhoneKeypadButton.Hangup:
                KeypadChannel.HangUpButtonPressed?.Invoke();
                break;
            case PhoneKeypadButton.Asterisk:
                KeypadChannel.AsteriskButtonPressed?.Invoke();
                break;
            case PhoneKeypadButton.Backspace:
                KeypadChannel.BackspacePressed?.Invoke();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(pressedButton), pressedButton, null);
        }
    }
}

public enum PhoneKeypadButton
{
    Key0,
    Key1,
    Key2,
    Key3,
    Key4,
    Key5,
    Key6,
    Key7,
    Key8,
    Key9,
    Confirm,
    Up,
    Down,
    Left,
    Right,
    LeftSelection,
    RightSelection,
    Pickup,
    Hangup,
    Asterisk,
    Backspace
}
