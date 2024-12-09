using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageScroller : MonoBehaviour
{
    [SerializeField] private KeypadChannel _keypadChannel;
    [SerializeField] private MessageSentChannel _messageSentChannel;
    [SerializeField] private ChatSelectionMenuChannel _chatSelectionMenuChannel;
    [SerializeField] private PromptChannel _promptChannel;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private FloatReference _scrollAmount;
    [SerializeField] private FloatReference _scrollFrequency;

    private Coroutine _scrollUpCoroutine;
    private Coroutine _scrollDownCoroutine;

    private void Awake()
    {
        _chatSelectionMenuChannel.GroupChatButtonPressed.AddListener(OnMessageReceived);
        _chatSelectionMenuChannel.MomChatButtonPressed.AddListener(OnMessageReceived);
        _chatSelectionMenuChannel.BoyfriendChatButtonPressed.AddListener(OnMessageReceived);
    }

    private void OnEnable()
    {
        _keypadChannel.UpButtonDown.AddListener(OnUpKeyDown);
        _keypadChannel.UpButtonUp.AddListener(OnUpKeyUp);
        _keypadChannel.DownButtonDown.AddListener(OnDownKeyDown);
        _keypadChannel.DownButtonUp.AddListener(OnDownKeyUp);
        _messageSentChannel.MessageSent.AddListener(OnMessageReceived);
        _promptChannel.promptStarted.AddListener(OnMessageReceived);
    }

    private void OnDisable()
    {
        _keypadChannel.UpButtonDown.RemoveListener(OnUpKeyDown);
        _keypadChannel.UpButtonUp.RemoveListener(OnUpKeyUp);
        _keypadChannel.DownButtonDown.RemoveListener(OnDownKeyDown);
        _keypadChannel.DownButtonUp.RemoveListener(OnDownKeyUp);
        _messageSentChannel.MessageSent.RemoveListener(OnMessageReceived);
        _promptChannel.promptStarted.RemoveListener(OnMessageReceived);

        StopScrolling();
    }

    private void OnDestroy()
    {
        _chatSelectionMenuChannel.GroupChatButtonPressed.RemoveListener(OnMessageReceived);
        _chatSelectionMenuChannel.MomChatButtonPressed.RemoveListener(OnMessageReceived);
        _chatSelectionMenuChannel.BoyfriendChatButtonPressed.RemoveListener(OnMessageReceived);
    }

    private void OnUpKeyDown()
    {
        StopScrolling();

        _scrollUpCoroutine = StartCoroutine(ScrollUp());
    }
    
    private void OnUpKeyUp()
    {
        StopScrolling();
    }

    private void OnDownKeyDown()
    {
        StopScrolling();

        _scrollDownCoroutine = StartCoroutine(ScrollDown());
    }
    
    private void OnDownKeyUp()
    {
        StopScrolling();
    }

    private void OnMessageReceived(MessageSender sender, bool isChatActive)
    {
        if (!isChatActive) return;

        _scrollRect.verticalNormalizedPosition = 0;
    }

    private void OnMessageReceived()
    {
        _scrollRect.verticalNormalizedPosition = 0;
    }

    private IEnumerator ScrollUp()
    {
        while (true)
        {
            float height = _scrollRect.content.rect.height;
            float verticalPosition = _scrollRect.verticalNormalizedPosition * height;
            float scrolledVerticalPosition = verticalPosition + _scrollAmount.Value;
            float normalizedScrolledPosition = scrolledVerticalPosition / height;
            float verticalNormalizedPosition = Mathf.Clamp(normalizedScrolledPosition, 0f, 1f);
            _scrollRect.verticalNormalizedPosition = verticalNormalizedPosition;

            yield return new WaitForSeconds(_scrollFrequency.Value);
        }
    }
    
    private IEnumerator ScrollDown()
    {
        while (true)
        {
            float height = _scrollRect.content.rect.height;
            float verticalPosition = _scrollRect.verticalNormalizedPosition * height;
            float scrolledVerticalPosition = verticalPosition - _scrollAmount.Value;
            float normalizedScrolledPosition = scrolledVerticalPosition / height;
            float verticalNormalizedPosition = Mathf.Clamp(normalizedScrolledPosition, 0f, 1f);
            _scrollRect.verticalNormalizedPosition = verticalNormalizedPosition;

            yield return new WaitForSeconds(_scrollFrequency.Value);
        }
    }

    private void StopScrolling()
    {
        if (_scrollUpCoroutine != null)
        {
            StopCoroutine(_scrollUpCoroutine);
        }
        
        if (_scrollDownCoroutine != null)
        {
            StopCoroutine(_scrollDownCoroutine);
        }
    }
}
