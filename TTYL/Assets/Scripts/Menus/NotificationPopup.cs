using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotificationPopup : MonoBehaviour
{
    [SerializeField] private GameObject _notificationPopupGO = null;
    [SerializeField] private GameObject _mailImageGO = null;
    [SerializeField] private GameObject _girlsMailGO = null;
    [SerializeField] private GameObject _girlsPopupGO = null;
    [SerializeField] private GameObject _momMailGO = null;
    [SerializeField] private GameObject _momPopupGO = null;
    [SerializeField] private GameObject _boyfriendMailGO = null;
    [SerializeField] private GameObject _boyfriendPopupGO = null;
    [SerializeField] private ChatSelectionMenuChannel _chatSelectionMenuChannel = null;
    [SerializeField] private MessageSentChannel _messageSentChannel = null;

    private TextMeshProUGUI _notificationText = null;
    private TextMeshProUGUI _girlsPopupText = null;
    private TextMeshProUGUI _momPopupText = null;
    private TextMeshProUGUI _boyfriendPopupText = null;
    private int _totalMessages = 0;
    private int _girlsMessages = 0;
    private int _momMessages = 0;
    private int _boyfriendMessages = 0;
    private Coroutine _blinkingEnvelope = null;

    private void Awake()
    {
        _notificationText = _notificationPopupGO.GetComponentInChildren<TextMeshProUGUI>();
        _girlsPopupText = _girlsPopupGO.GetComponentInChildren<TextMeshProUGUI>();
        _momPopupText = _momPopupGO.GetComponentInChildren<TextMeshProUGUI>();
        _boyfriendPopupText = _boyfriendPopupGO.GetComponentInChildren<TextMeshProUGUI>();

        _mailImageGO.SetActive(false);
        _momMailGO.SetActive(false);
        _girlsMailGO.SetActive(false);
        _boyfriendMailGO.SetActive(false);
    }

    private void OnEnable()
    {
        _messageSentChannel.MessageSent.AddListener(OnNotificationReceived);
        _chatSelectionMenuChannel.GroupChatButtonPressed.AddListener(OnGroupChatPressed);
        _chatSelectionMenuChannel.MomChatButtonPressed.AddListener(OnMomChatPressed);
        _chatSelectionMenuChannel.BoyfriendChatButtonPressed.AddListener(OnBoyfriendChatPressed);
    }

    private void OnDisable()
    {
        _messageSentChannel.MessageSent.RemoveListener(OnNotificationReceived);
        _chatSelectionMenuChannel.GroupChatButtonPressed.RemoveListener(OnGroupChatPressed);
        _chatSelectionMenuChannel.MomChatButtonPressed.RemoveListener(OnMomChatPressed);
        _chatSelectionMenuChannel.BoyfriendChatButtonPressed.RemoveListener(OnBoyfriendChatPressed);
    }

    private void OnNotificationReceived(MessageSender sender, bool isChatActive)
    {
        if (isChatActive) return;

        switch (sender)
        {
            case MessageSender.Mom:
                _momMailGO.SetActive(true);
                _momMessages++;
                _momPopupText.text = _momMessages.ToString();
                break;
            case MessageSender.Jess:
            case MessageSender.Rachel:
            case MessageSender.Megan:
                _girlsMailGO.SetActive(true);
                _girlsMessages++;
                _girlsPopupText.text =_girlsMessages.ToString();
                break;
            case MessageSender.Boyfriend:
                _boyfriendMailGO.SetActive(true);
                _boyfriendMessages++;
                _boyfriendPopupText.text = _boyfriendMessages.ToString();
                break;
        }

        _totalMessages = _girlsMessages + _momMessages + _boyfriendMessages;

        if (!_mailImageGO.activeSelf)
        {
            _mailImageGO.SetActive(true);
        }
        else
        {
            if (_blinkingEnvelope != null)
                StopCoroutine(BlinkEnvelope());

            _blinkingEnvelope = StartCoroutine(BlinkEnvelope());
        }

        _notificationText.text = _totalMessages.ToString();
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
                _totalMessages -= _girlsMessages;
                _girlsMessages = 0;
                _girlsMailGO.SetActive(false);
                break;
            case Chat.Mom:
                _totalMessages -= _momMessages;
                _momMessages = 0;
                _momMailGO.SetActive(false);
                break; 
            case Chat.Boyfriend:
                _totalMessages -= _boyfriendMessages;
                _boyfriendMessages = 0;
                _boyfriendMailGO.SetActive(false);
                break;
        }

        if (_totalMessages <= 0)
        {
            _mailImageGO.SetActive(false);
            _totalMessages = 0;
            return;
        }

        _notificationText.text = _totalMessages.ToString();
    }

    private IEnumerator BlinkEnvelope()
    {
        Image image = _mailImageGO.GetComponent<Image>();
        Image popupImage = _notificationPopupGO.GetComponentInChildren<Image>();
        TextMeshProUGUI text = _notificationPopupGO.GetComponentInChildren<TextMeshProUGUI>();

        if (image == null || popupImage == null || text == null) yield break;

        Color newColor = image.color;
        Color popupColor = popupImage.color;
        Color textColor = text.color;

        newColor.a = 0;
        popupColor.a = 0;
        textColor.a = 0;
        image.color = newColor;
        popupImage.color = popupColor;
        text.color = textColor;

        yield return null;

        float t = 0;

        while(t < 1f)
        {
            t += Time.deltaTime;

            newColor.a = Mathf.Lerp(0, 1, t);
            popupColor.a = Mathf.Lerp(0, 1, t);
            textColor.a = Mathf.Lerp(0, 1, t);

            image.color = newColor;
            popupImage.color = popupColor;
            text.color = textColor;

            yield return null;
        }
    }
}

public enum Chat
{ 
    Girls,
    Mom,
    Boyfriend,
}
