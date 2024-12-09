using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuBase : MonoBehaviour
{
    [ShowInInspector] private int _buttonIndex;
    [SerializeField] protected GameObject[] ButtonGameObjects;
    [SerializeField] protected Button LeftSelectionButton;
    [SerializeField] protected Button RightSelectionButton;
    [SerializeField] protected KeypadChannel KeypadChannel;
    [SerializeField] protected EventSystem EventSystem;
    [SerializeField] protected GameObject PauseGO;
    protected Button[] Buttons;
    protected OnScreenButton[] OnScreenButtons;

    [BoxGroup("Overrides")] [SerializeField] private int _startingButtonIndex = 0;

    protected int ButtonIndex
    {
        get => _buttonIndex;
        set
        {
            int previousIndex = _buttonIndex;
            _buttonIndex = value;
            OnScreenButtons[previousIndex].SetUnSelected();
            SetButtonActive(value);
        }
    }
    
    private void Awake()
    {
        PauseGO.SetActive(false);
        InitializeButtonArray();
    }

    protected virtual void OnEnable()
    {
        Debug.Log($"{gameObject.name} Enabled");
        ResetButtonHighlights();
        SetButtonActive(ButtonIndex);
        RegisterButtonHandlers();
    }

    private void Start()
    {
        ButtonIndex = _startingButtonIndex;
    }

    [Button]
    private void RegisterButtonHandlers()
    {
        Debug.Log($"{gameObject.name} Registering button handlers");
        KeypadChannel.UpButtonPressed?.AddListener(OnUpKeyPressed);
        KeypadChannel.DownButtonPressed?.AddListener(OnDownKeyPressed);
        KeypadChannel.LeftButtonPressed?.AddListener(OnLeftKeyPressed);
        KeypadChannel.RightButtonPressed?.AddListener(OnRightKeyPressed);
        KeypadChannel.ConfirmButtonPressed?.AddListener(OnConfirmKeyPressed);
        KeypadChannel.LeftSelectionButtonPressed?.AddListener(OnLeftSelectionKeyPressed);
        KeypadChannel.RightSelectionButtonPressed?.AddListener(OnRightSelectionKeyPressed);
        KeypadChannel.GamePaused?.AddListener(OnGamePaused);
    }

    protected virtual void OnDisable()
    {
        UnregisterButtonHandlers();
    }

    private void UnregisterButtonHandlers()
    {
        KeypadChannel.UpButtonPressed?.RemoveListener(OnUpKeyPressed);
        KeypadChannel.DownButtonPressed?.RemoveListener(OnDownKeyPressed);
        KeypadChannel.LeftButtonPressed?.RemoveListener(OnLeftKeyPressed);
        KeypadChannel.RightButtonPressed?.RemoveListener(OnRightKeyPressed);
        KeypadChannel.ConfirmButtonPressed?.RemoveListener(OnConfirmKeyPressed);
        KeypadChannel.LeftSelectionButtonPressed?.RemoveListener(OnLeftSelectionKeyPressed);
        KeypadChannel.RightSelectionButtonPressed?.RemoveListener(OnRightSelectionKeyPressed);
        KeypadChannel.GamePaused?.RemoveListener(OnGamePaused);
    }

    private void InitializeButtonArray()
    {
        Buttons = new Button[ButtonGameObjects.Length];
        OnScreenButtons = new OnScreenButton[ButtonGameObjects.Length];

        for (int i = 0; i < ButtonGameObjects.Length; i++)
        {
            Buttons[i] = ButtonGameObjects[i].GetComponent<Button>();
            OnScreenButtons[i] = ButtonGameObjects[i].GetComponent<OnScreenButton>();
        }
    }

    protected void SetButtonActive(int index)
    {
        EventSystem.SetSelectedGameObject(ButtonGameObjects[index]);
        OnScreenButtons[index].SetSelected();
    }

    protected void ResetButtonHighlights()
    {
        foreach (OnScreenButton onScreenButton in OnScreenButtons)
        {
            onScreenButton.SetUnSelected();
        }
    }

    protected void ConfirmActiveButton()
    {
        Buttons[ButtonIndex].onClick.Invoke();
    }

    protected virtual void OnUpKeyPressed()
    {
        
    }
    
    protected virtual void OnDownKeyPressed()
    {
        
    }
    
    protected virtual void OnLeftKeyPressed()
    {
        
    }
    
    protected virtual void OnRightKeyPressed()
    {
        
    }
    
    protected virtual void OnConfirmKeyPressed()
    {
        ConfirmActiveButton();
    }
    
    protected virtual void OnLeftSelectionKeyPressed()
    {
        
    }
    
    protected virtual void OnRightSelectionKeyPressed()
    {
        
    }

    private void OnGamePaused(bool isPaused)
    {
        if (isPaused)
            PauseGO.SetActive(true);
        else
            PauseGO.SetActive(false);
    }
}
