using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{

    [FoldoutGroup("Channels")]
    [SerializeField] private MessageSentChannel _messageSentChannel = null;
    [FoldoutGroup("Channels")]
    [SerializeField] private ChatSelectionMenuChannel _chatSelectionMenuChannel = null;
    [FoldoutGroup("Channels")]
    [SerializeField] private MessageDisplayChannel _messageDisplayChannel = null;

    [FoldoutGroup("ChatSelectionMenu")]
    [SerializeField] private GameObject _girlsMenuNotificationGO = null;
    [FoldoutGroup("ChatSelectionMenu")]
    [SerializeField] private GameObject _momMenuNotificationGO = null;
    [FoldoutGroup("ChatSelectionMenu")]
    [SerializeField] private GameObject _boyfriendMenuNotificationGO = null;

    [FoldoutGroup("Chats")]
    [SerializeField] private GameObject[] _notificationGOs = null;

    private int _totalMessages = 0;
    private int _girlsMessages = 0;
    private int _momMessages = 0;
    private int _boyfriendMessages = 0;

    private GameObject _globalNotificationGO = null;

    private TextMeshProUGUI _globalCounterText = null;
    private TextMeshProUGUI _girlsCounterText = null;
    private TextMeshProUGUI _momCounterText = null;
    private TextMeshProUGUI _boyfriendCounterText = null;

    private Coroutine _globalBlink = null;
    private Coroutine _girlsBlink = null;
    private Coroutine _momBlink = null;
    private Coroutine _boyfriendBlink = null;

    private void Awake()
    {
        _girlsCounterText = _girlsMenuNotificationGO.GetComponentInChildren<TextMeshProUGUI>();
        _momCounterText = _momMenuNotificationGO.GetComponentInChildren<TextMeshProUGUI>();
        _boyfriendCounterText = _boyfriendMenuNotificationGO.GetComponentInChildren<TextMeshProUGUI>();

        foreach (GameObject go in _notificationGOs)
            go.SetActive(false);

        _girlsMenuNotificationGO.SetActive(false);
        _momMenuNotificationGO.SetActive(false);
        _boyfriendMenuNotificationGO.SetActive(false);
    }

    private void OnEnable()
    {
        _messageSentChannel.MessageSent.AddListener(OnMessageReceived);
        _messageDisplayChannel.BackButtonPressed.AddListener(OnChatSelectionActive);
        _chatSelectionMenuChannel.GroupChatButtonPressed.AddListener(OnGroupChatPressed);
        _chatSelectionMenuChannel.MomChatButtonPressed.AddListener(OnMomChatPressed);
        _chatSelectionMenuChannel.BoyfriendChatButtonPressed.AddListener(OnBoyfriendChatPressed);
    }

    private void OnDisable()
    {
        _messageSentChannel.MessageSent.RemoveListener(OnMessageReceived);
        _messageDisplayChannel.BackButtonPressed.RemoveListener(OnChatSelectionActive);
        _chatSelectionMenuChannel.GroupChatButtonPressed.RemoveListener(OnGroupChatPressed);
        _chatSelectionMenuChannel.MomChatButtonPressed.RemoveListener(OnMomChatPressed);
        _chatSelectionMenuChannel.BoyfriendChatButtonPressed.RemoveListener(OnBoyfriendChatPressed);
    }

    private void OnMessageReceived(MessageSender sender, bool isChatActive)
    {
        if (isChatActive) return;

        switch(sender)
        {
            case MessageSender.Jess:
            case MessageSender.Megan:
            case MessageSender.Rachel:
                _girlsMessages++;
                _girlsCounterText.text = _girlsMessages.ToString();

                if(!_girlsMenuNotificationGO.activeSelf) _girlsMenuNotificationGO.SetActive(true);
                else
                {
                    if (_girlsBlink != null) StopCoroutine(_girlsBlink);

                    _girlsBlink = StartCoroutine(BlinkEnvelope(_girlsMenuNotificationGO));
                }

                break;

            case MessageSender.Mom:
                _momMessages++;
                _momCounterText.text = _momMessages.ToString();

                if (!_momMenuNotificationGO.activeSelf) _momMenuNotificationGO.SetActive(true);
                else
                {
                    if (_momBlink != null) StopCoroutine(_momBlink);

                    _momBlink = StartCoroutine(BlinkEnvelope(_momMenuNotificationGO));
                }

                break;

            case MessageSender.Boyfriend:
                _boyfriendMessages++;
                _boyfriendCounterText.text = _boyfriendMessages.ToString();

                if (!_boyfriendMenuNotificationGO.activeSelf) _boyfriendMenuNotificationGO.SetActive(true);
                else
                {
                    if (_boyfriendBlink != null) StopCoroutine(_boyfriendBlink);

                    _boyfriendBlink = StartCoroutine(BlinkEnvelope(_boyfriendMenuNotificationGO));
                }

                break;
        }

        _totalMessages = _girlsMessages + _momMessages + _boyfriendMessages;

        #if !UNITY_EDITOR && !PLATFORM_WEBGL
            if (_totalMessages == 1) 
                    Handheld.Vibrate();
        #endif

        #if UNITY_EDITOR || PLATFORM_WEBGL
            if (_totalMessages == 1) 
                _messageSentChannel.TriggerHapticFeedback?.Invoke();
        #endif

        if (_globalNotificationGO == null) return;

        if (!_globalNotificationGO.activeSelf) _globalNotificationGO.SetActive(true);
        else
        {
            if (_globalBlink != null) StopCoroutine(_globalBlink);

            _globalBlink = StartCoroutine(BlinkEnvelope(_globalNotificationGO));
        }

        if(_globalCounterText == null) 
            _globalCounterText = _globalNotificationGO.GetComponentInChildren<TextMeshProUGUI>();

        _globalCounterText.text = _totalMessages.ToString();
    }

    private void OnChatSelectionActive()
    {
        if (_globalNotificationGO == null) return;

        _globalNotificationGO.SetActive(false);
        _globalNotificationGO = null;
        _globalCounterText = null;
    }

    private void OnGroupChatPressed()
    {
        HandleChatSwitch(Chat.Girls);
    }

    private void OnMomChatPressed()
    {
        HandleChatSwitch(Chat.Mom);
    }

    private void OnBoyfriendChatPressed()
    {
        HandleChatSwitch(Chat.Boyfriend);
    }

    private void HandleChatSwitch(Chat chat)
    {
        switch (chat)
        {
            case Chat.Girls:
                _globalNotificationGO = _notificationGOs[0];
                _totalMessages -= _girlsMessages;
                _girlsMessages = 0;
                _girlsMenuNotificationGO.SetActive(false);
                break;
            case Chat.Mom:
                _globalNotificationGO = _notificationGOs[1];
                _totalMessages -= _momMessages;
                _momMessages = 0;
                _momMenuNotificationGO.SetActive(false);
                break;
            case Chat.Boyfriend:
                _globalNotificationGO = _notificationGOs[2];
                _totalMessages -= _boyfriendMessages;
                _boyfriendMessages = 0;
                _boyfriendMenuNotificationGO.SetActive(false);
                break;
        }

        _globalCounterText = _globalNotificationGO.GetComponentInChildren<TextMeshProUGUI>();
        _globalCounterText.text = _totalMessages.ToString();

        if (_totalMessages <= 0)
        {
            _globalNotificationGO.SetActive(false);
            _totalMessages = 0;
        }
        else
        {
            _globalNotificationGO.SetActive(true);
        }
    }

    private IEnumerator BlinkEnvelope(GameObject go)
    {
        Image envelopeImage = go.GetComponent<Image>();
        Image textBackground = go.GetComponentInChildren<Image>();
        TextMeshProUGUI text = go.GetComponentInChildren<TextMeshProUGUI>();

        if (envelopeImage == null || textBackground == null || text == null) yield break;

        Color newColor = envelopeImage.color;
        Color backgroundColor = textBackground.color;
        Color textColor = text.color;

        newColor.a = 0;
        backgroundColor.a = 0;
        textColor.a = 0;
        envelopeImage.color = newColor;
        textBackground.color = backgroundColor;
        text.color = textColor;

        yield return null;

        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime;

            newColor.a = Mathf.Lerp(0, 1, t);
            backgroundColor.a = Mathf.Lerp(0, 1, t);
            textColor.a = Mathf.Lerp(0, 1, t);

            envelopeImage.color = newColor;
            textBackground.color = backgroundColor;
            text.color = textColor;

            yield return null;
        }
    }
}
