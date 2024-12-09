using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class PhoneDisplayCanvasController : MonoBehaviour
{
    [BoxGroup("Canvases")] [SerializeField] private GameObject _groupChatCanvas;
    [BoxGroup("Canvases")] [SerializeField] private GameObject _momChatCanvas;
    [BoxGroup("Canvases")] [SerializeField] private GameObject _boyfriendChatCanvas;
    [BoxGroup("Canvases")] [SerializeField] private GameObject _chatSelectionCanvas;
    [BoxGroup("Canvases")] [SerializeField] private GameObject _gameOverCanvas;

    [BoxGroup("Channels")] [SerializeField]
    private DisplayChannel _displayChannel;
    
    [BoxGroup("Channels")] [SerializeField]
    private ChatSelectionMenuChannel _chatSelectionMenuChannel;

    [BoxGroup("Channels")] [SerializeField]
    private MessageDisplayChannel _messageDisplayChannel;

    [field: SerializeField] public GameObject ActiveDisplay { get; private set; }

    private void Awake()
    {
        SetAllDisplaysInactive();
        ActiveDisplay.SetActive(true);
    }

    private void OnEnable()
    {
        _chatSelectionMenuChannel.GroupChatButtonPressed.AddListener(SetGroupChatActive);
        _chatSelectionMenuChannel.MomChatButtonPressed.AddListener(SetMomChatActive);
        _chatSelectionMenuChannel.BoyfriendChatButtonPressed.AddListener(SetBoyfriendChatActive);
        _messageDisplayChannel.BackButtonPressed.AddListener(SetChatSelectionActive);
    }

    private void OnDisable()
    {
        _chatSelectionMenuChannel.GroupChatButtonPressed.RemoveListener(SetGroupChatActive);
        _chatSelectionMenuChannel.MomChatButtonPressed.RemoveListener(SetMomChatActive);
        _chatSelectionMenuChannel.BoyfriendChatButtonPressed.RemoveListener(SetBoyfriendChatActive);
        _messageDisplayChannel.BackButtonPressed.RemoveListener(SetChatSelectionActive);
    }

    [Button]
    public void SetChatSelectionActive()
    {
        ActiveDisplay.SetActive(false);
        ActiveDisplay = _chatSelectionCanvas;
        ActiveDisplay.SetActive(true);
        _displayChannel.ChatSelectionActive?.Invoke();
    }
    
    [Button]
    public void SetGroupChatActive()
    {
        ActiveDisplay.SetActive(false);
        ActiveDisplay = _groupChatCanvas;
        ActiveDisplay.SetActive(true);
        _displayChannel.GroupChatActive?.Invoke();
    }

    [Button]
    public void SetMomChatActive()
    {
        ActiveDisplay.SetActive(false);
        ActiveDisplay = _momChatCanvas;
        ActiveDisplay.SetActive(true);
        _displayChannel.MomChatActive?.Invoke();
    }
    
    [Button]
    public void SetBoyfriendChatActive()
    {
        ActiveDisplay.SetActive(false);
        ActiveDisplay = _boyfriendChatCanvas;
        ActiveDisplay.SetActive(true);
        _displayChannel.BoyfriendChatActive?.Invoke();
    }

    [Button]
    public void SetGameOverActive()
    {
        ActiveDisplay.SetActive(false);
        ActiveDisplay = _gameOverCanvas;
        ActiveDisplay.SetActive(true);
        _displayChannel.GameOverActive?.Invoke();
    }

    private void SetAllDisplaysInactive()
    {
        _groupChatCanvas.SetActive(false);
        _momChatCanvas.SetActive(false);
        _boyfriendChatCanvas.SetActive(false);
        _gameOverCanvas.SetActive(false);
        _chatSelectionCanvas.SetActive(false);
    }
}
