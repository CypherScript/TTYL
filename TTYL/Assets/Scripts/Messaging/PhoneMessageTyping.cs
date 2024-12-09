using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PhoneMessageTyping : MonoBehaviour
{
    [SerializeField] protected PhoneTextField InputField;
    [SerializeField] protected KeypadButton[] KeypadButtons;
    [SerializeField] protected float TimeToSwitchChar = 1f;
    [SerializeField] protected KeypadChannel KeypadChannel;
    
    protected int? PreviousNumKeyPress = null;
    protected int LetterIndex;
    protected float _numpadTimer;
    private bool _isTimerActive;

    protected virtual void OnEnable()
    {
        RegisterCallbacks();
    }

    protected virtual void OnDisable()
    {
        UnregisterCallbacks();
    }

    private void RegisterCallbacks()
    {
        KeypadChannel.Keypad0Pressed?.AddListener(OnKeyPressed0);
        KeypadChannel.Keypad1Pressed?.AddListener(OnKeyPressed1);
        KeypadChannel.Keypad2Pressed?.AddListener(OnKeyPressed2);
        KeypadChannel.Keypad3Pressed?.AddListener(OnKeyPressed3);
        KeypadChannel.Keypad4Pressed?.AddListener(OnKeyPressed4);
        KeypadChannel.Keypad5Pressed?.AddListener(OnKeyPressed5);
        KeypadChannel.Keypad6Pressed?.AddListener(OnKeyPressed6);
        KeypadChannel.Keypad7Pressed?.AddListener(OnKeyPressed7);
        KeypadChannel.Keypad8Pressed?.AddListener(OnKeyPressed8);
        KeypadChannel.Keypad9Pressed?.AddListener(OnKeyPressed9);
        
        KeypadChannel.UpButtonPressed?.AddListener(OnUpArrowPressed);
        KeypadChannel.DownButtonPressed?.AddListener(OnDownArrowPressed);
        KeypadChannel.LeftButtonPressed?.AddListener(OnLeftArrowPressed);
        KeypadChannel.RightButtonPressed?.AddListener(OnRightArrowPressed);
        KeypadChannel.ConfirmButtonPressed?.AddListener(OnConfirmPressed);
        KeypadChannel.LeftSelectionButtonPressed?.AddListener(OnLeftSelectionKeyPressed);
        //KeypadChannel.RightSelectionButtonPressed?.AddListener(OnRightSelectionKeyPressed);
        KeypadChannel.RightSelectionButtonPressed?.AddListener(OnKeyPressedBackspace);
        //KeypadChannel.BackspacePressed?.AddListener(OnKeyPressedBackspace);
        KeypadChannel.AsteriskButtonPressed?.AddListener(OnAsteriskKeyPressed);
        KeypadChannel.PickupButtonPressed?.AddListener(OnPickupKeyPressed);
        KeypadChannel.HangUpButtonPressed?.AddListener(OnHangupKeyPressed);
    }

    private void UnregisterCallbacks()
    {
        KeypadChannel.Keypad0Pressed?.RemoveListener(OnKeyPressed0);
        KeypadChannel.Keypad1Pressed?.RemoveListener(OnKeyPressed1);
        KeypadChannel.Keypad2Pressed?.RemoveListener(OnKeyPressed2);
        KeypadChannel.Keypad3Pressed?.RemoveListener(OnKeyPressed3);
        KeypadChannel.Keypad4Pressed?.RemoveListener(OnKeyPressed4);
        KeypadChannel.Keypad5Pressed?.RemoveListener(OnKeyPressed5);
        KeypadChannel.Keypad6Pressed?.RemoveListener(OnKeyPressed6);
        KeypadChannel.Keypad7Pressed?.RemoveListener(OnKeyPressed7);
        KeypadChannel.Keypad8Pressed?.RemoveListener(OnKeyPressed8);
        KeypadChannel.Keypad9Pressed?.RemoveListener(OnKeyPressed9);
        
        KeypadChannel.UpButtonPressed?.RemoveListener(OnUpArrowPressed);
        KeypadChannel.DownButtonPressed?.RemoveListener(OnDownArrowPressed);
        KeypadChannel.LeftButtonPressed?.RemoveListener(OnLeftArrowPressed);
        KeypadChannel.RightButtonPressed?.RemoveListener(OnRightArrowPressed);
        KeypadChannel.ConfirmButtonPressed?.RemoveListener(OnConfirmPressed);
        KeypadChannel.LeftSelectionButtonPressed?.RemoveListener(OnLeftSelectionKeyPressed);
        //KeypadChannel.RightSelectionButtonPressed?.RemoveListener(OnRightSelectionKeyPressed);
        KeypadChannel.RightSelectionButtonPressed?.RemoveListener(OnKeyPressedBackspace);
        //KeypadChannel.BackspacePressed?.RemoveListener(OnKeyPressedBackspace);
        KeypadChannel.AsteriskButtonPressed?.RemoveListener(OnAsteriskKeyPressed);
        KeypadChannel.PickupButtonPressed?.RemoveListener(OnPickupKeyPressed);
        KeypadChannel.HangUpButtonPressed?.RemoveListener(OnHangupKeyPressed);
    }

    private void Update()
    {
        NumPadTimer();
    }

    private void NumPadTimer()
    {
        if (!_isTimerActive)
        {
            return;
        }

        if (_numpadTimer > Time.time)
        {
            return;
        }

        HandleNumpadTimerExpired();
    }

    private void HandleNumpadTimerExpired()
    {
        _isTimerActive = false;
        PreviousNumKeyPress = null;
        LetterIndex = 0;
        
        InputField.ShowTextCursor();
    }

    private void CycleLetterIndex(int keyNumber)
    {
        //Restart the timer
        if (_isTimerActive)
        {
            _isTimerActive = false;
        }
        _numpadTimer = Time.time + TimeToSwitchChar;
        _isTimerActive = true;
        
        LetterIndex++;
        //wrap index if last letter is reached
        if (LetterIndex >= KeypadButtons[keyNumber].Letters.Count)
        {
            LetterIndex = 0;
        }

        string nextLetter = KeypadButtons[keyNumber].Letters[LetterIndex];
        //replace the last char in message with the next letter in key
        char nextChar = nextLetter.ToCharArray()[0];
        char[] message = InputField.Text.ToCharArray();
        message[InputField.Text.Length-1] = nextChar;
        InputField.Text = new string(message);
        InputField.ActiveCharacter.Text = nextLetter;
    }
    
    private void NewKeyPressed(int keyNumber)
    {
        if (_isTimerActive)
        {
            _isTimerActive = false;
        }

        if (InputField.IsAtCharacterLimit)
        {
            InputField.ShowTextCursor();
            return;
        }

        PreviousNumKeyPress = keyNumber;
        LetterIndex = 0;
        var nextLetter = KeypadButtons[keyNumber].Letters[0];
        
        InputField.CreateNextCharacter(nextLetter);
        
        InputField.Text += nextLetter;
        _numpadTimer = Time.time + TimeToSwitchChar;
        _isTimerActive = true;
    }

    //Space key
    public virtual void OnKeyPressed0()
    {
        if (InputField.IsAtCharacterLimit)
        {
            return;
        }
        if (_isTimerActive)
        {
            _isTimerActive = false;
        }

        LetterIndex = 0;
        var nextLetter = KeypadButtons[0].Letters[0];
        PreviousNumKeyPress = null;

        InputField.CreateNextCharacter(nextLetter);
        InputField.ShowTextCursor();

        InputField.Text += nextLetter;
    }
    
    public virtual void OnKeyPressed1()
    {
        if (PreviousNumKeyPress is not 1)
        {
            NewKeyPressed(1);
            return;
        }
        
        CycleLetterIndex(1);
    }
    
    public virtual void OnKeyPressed2()
    {
        if (PreviousNumKeyPress is not 2)
        {
            NewKeyPressed(2);
            return;
        }
        
        CycleLetterIndex(2);
    }
    
    public virtual void OnKeyPressed3()
    {
        if (PreviousNumKeyPress is not 3)
        {
            NewKeyPressed(3);
            return;
        }
        
        CycleLetterIndex(3);
    }
    
    public virtual void OnKeyPressed4()
    {
        if (PreviousNumKeyPress is not 4)
        {
            NewKeyPressed(4);
            return;
        }
        
        CycleLetterIndex(4);
    }
    
    public virtual void OnKeyPressed5()
    {
        if (PreviousNumKeyPress is not 5)
        {
            NewKeyPressed(5);
            return;
        }
        
        CycleLetterIndex(5);
    }
    
    public virtual void OnKeyPressed6()
    {
        if (PreviousNumKeyPress is not 6)
        {
            NewKeyPressed(6);
            return;
        }
        
        CycleLetterIndex(6);
    }
    
    public virtual void OnKeyPressed7()
    {
        if (PreviousNumKeyPress is not 7)
        {
            NewKeyPressed(7);
            return;
        }
        
        CycleLetterIndex(7);
    }
    
    public virtual void OnKeyPressed8()
    {
        if (PreviousNumKeyPress is not 8)
        {
            NewKeyPressed(8);
            return;
        }
        
        CycleLetterIndex(8);
    }
    
    public virtual void OnKeyPressed9()
    {
        if (PreviousNumKeyPress is not 9)
        {
            NewKeyPressed(9);
            return;
        }
        
        CycleLetterIndex(9);
    }

    public virtual void OnKeyPressedBackspace()
    {
        PreviousNumKeyPress = null;
        InputField.DeleteLastCharacter();

        if (InputField.Text.Length > 0)
        {
            InputField.Text = InputField.Text.Remove(InputField.Text.Length - 1);
        }
    }
    
    public virtual void OnConfirmPressed()
    {
        
    }
    
    public virtual void OnUpArrowPressed()
    {
        
    }
    
    public virtual void OnDownArrowPressed()
    {
        
    }
    
    public virtual void OnLeftArrowPressed()
    {
        
    }
    
    public virtual void OnRightArrowPressed()
    {
        if (_isTimerActive)
        {
            _isTimerActive = false;
        }
        else
        {
            return;
        }
        
        PreviousNumKeyPress = null;
        LetterIndex = 0;
        
        InputField.ShowTextCursor();
    }

    public virtual void OnLeftSelectionKeyPressed()
    {
        
    }

    public virtual void OnRightSelectionKeyPressed()
    {
        Clear();
    }
    
    public virtual void OnAsteriskKeyPressed()
    {
        if (PreviousNumKeyPress is not 10)
        {
            NewKeyPressed(10);
            return;
        }

        CycleLetterIndex(10);
    }
    
    public virtual void OnPickupKeyPressed()
    {
        
    }
    
    public virtual void OnHangupKeyPressed()
    {
        
    }
    
    public void Clear()
    {
        InputField.Clear();
    }
}


