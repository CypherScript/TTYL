using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerPhoneInput : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private bool _shouldInvertKeypad;
    [SerializeField] private KeypadActionMapping _keypadActionMapping;

    private InputAction _keypad0Action;
    private InputAction _keypad1Action;
    private InputAction _keypad2Action;
    private InputAction _keypad3Action;
    private InputAction _keypad4Action;
    private InputAction _keypad5Action;
    private InputAction _keypad6Action;
    private InputAction _keypad7Action;
    private InputAction _keypad8Action;
    private InputAction _keypad9Action;
    private InputAction _backspaceAction;
    private InputAction _rightButtonAction;
    private InputAction _confirmButtonAction;
    
    private void Awake()
    {
        if (_shouldInvertKeypad)
        {
            BindKeypadActionMapInverted();
        }
        else
        {
            BindKeypadActionMap();
        }
        
        _backspaceAction = _playerInput.actions["Backspace"];
        _rightButtonAction = _playerInput.actions["Right"];
        _confirmButtonAction = _playerInput.actions["Confirm"];
        
        _backspaceAction.performed += OnBackspacePerformed;
        _rightButtonAction.performed += OnRightArrowPerformed;
        _confirmButtonAction.performed += OnConfirmPerformed;
    }

    private void OnEnable()
    {
        RegisterKeyboardInputCallbacks();
    }

    private void OnDestroy()
    {
        UnregisterKeyboardInputCallbacks();
    }

    private void RegisterKeyboardInputCallbacks()
    {
        _keypad0Action.performed += OnKeypad0Performed;
        _keypad1Action.performed += OnKeypad1Performed;
        _keypad2Action.performed += OnKeypad2Performed;
        _keypad3Action.performed += OnKeypad3Performed;
        _keypad4Action.performed += OnKeypad4Performed;
        _keypad5Action.performed += OnKeypad5Performed;
        _keypad6Action.performed += OnKeypad6Performed;
        _keypad7Action.performed += OnKeypad7Performed;
        _keypad8Action.performed += OnKeypad8Performed;
        _keypad9Action.performed += OnKeypad9Performed;
    }

    private void UnregisterKeyboardInputCallbacks()
    {
        _keypad0Action.performed -= OnKeypad0Performed;
        _keypad1Action.performed -= OnKeypad1Performed;
        _keypad2Action.performed -= OnKeypad2Performed;
        _keypad3Action.performed -= OnKeypad3Performed;
        _keypad4Action.performed -= OnKeypad4Performed;
        _keypad5Action.performed -= OnKeypad5Performed;
        _keypad6Action.performed -= OnKeypad6Performed;
        _keypad7Action.performed -= OnKeypad7Performed;
        _keypad8Action.performed -= OnKeypad8Performed;
        _keypad9Action.performed -= OnKeypad9Performed;
    }

    private void OnKeypad0Performed(InputAction.CallbackContext context)
    {
        _keypadActionMapping.OnKeyPressed0();
    }
    
    private void OnKeypad1Performed(InputAction.CallbackContext context)
    {
        _keypadActionMapping.OnKeyPressed1();
    }
    
    private void OnKeypad2Performed(InputAction.CallbackContext context)
    {
        _keypadActionMapping.OnKeyPressed2();
    }
    
    private void OnKeypad3Performed(InputAction.CallbackContext context)
    {
        _keypadActionMapping.OnKeyPressed3();
    }
    
    private void OnKeypad4Performed(InputAction.CallbackContext context)
    {
        _keypadActionMapping.OnKeyPressed4();
    }
    
    private void OnKeypad5Performed(InputAction.CallbackContext context)
    {
        _keypadActionMapping.OnKeyPressed5();
    }
    
    private void OnKeypad6Performed(InputAction.CallbackContext context)
    {
        _keypadActionMapping.OnKeyPressed6();
    }
    
    private void OnKeypad7Performed(InputAction.CallbackContext context)
    {
        _keypadActionMapping.OnKeyPressed7();
    }
    
    private void OnKeypad8Performed(InputAction.CallbackContext context)
    {
        _keypadActionMapping.OnKeyPressed8();
    }
    
    private void OnKeypad9Performed(InputAction.CallbackContext context)
    {
        _keypadActionMapping.OnKeyPressed9();
    }

    private void OnBackspacePerformed(InputAction.CallbackContext context)
    {
        _keypadActionMapping.OnKeyPressedBackspace();
    }
    
    private void OnRightArrowPerformed(InputAction.CallbackContext context)
    {
        _keypadActionMapping.OnRightArrowPressed();
    }
    
    private void OnConfirmPerformed(InputAction.CallbackContext context)
    {
        _keypadActionMapping.OnConfirmPressed();
    }

    [Button]
    public void BindKeypadActionMap()
    {
        _keypad0Action = _playerInput.actions["0"];
        _keypad1Action = _playerInput.actions["1"];
        _keypad2Action = _playerInput.actions["2"];
        _keypad3Action = _playerInput.actions["3"];
        _keypad4Action = _playerInput.actions["4"];
        _keypad5Action = _playerInput.actions["5"];
        _keypad6Action = _playerInput.actions["6"];
        _keypad7Action = _playerInput.actions["7"];
        _keypad8Action = _playerInput.actions["8"];
        _keypad9Action = _playerInput.actions["9"];
    }

    [Button]
    public void BindKeypadActionMapInverted()
    {
        _keypad0Action = _playerInput.actions["0"];
        _keypad1Action = _playerInput.actions["7"];
        _keypad2Action = _playerInput.actions["8"];
        _keypad3Action = _playerInput.actions["9"];
        _keypad4Action = _playerInput.actions["4"];
        _keypad5Action = _playerInput.actions["5"];
        _keypad6Action = _playerInput.actions["6"];
        _keypad7Action = _playerInput.actions["1"];
        _keypad8Action = _playerInput.actions["2"];
        _keypad9Action = _playerInput.actions["3"];
    }

    public void RebindStandard()
    {
        UnregisterKeyboardInputCallbacks();
        BindKeypadActionMap();
        RegisterKeyboardInputCallbacks();
    }

    public void RebindInverted()
    {
        UnregisterKeyboardInputCallbacks();
        BindKeypadActionMapInverted();
        RegisterKeyboardInputCallbacks();
    }
}
