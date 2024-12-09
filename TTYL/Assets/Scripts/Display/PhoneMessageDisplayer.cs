using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

public class PhoneMessageDisplayer : DialogueViewBase
{
    
    [SerializeField] protected DialogueRunner DialogueRunner;
    [SerializeField] private GameObject _contentContainer;
    [SerializeField] private int _maxElementCount;
    [SerializeField] private MessageSentChannel _messageSentChannel;
    [SerializeField] protected MessageBubbleSettings BubbleSettings;

    [BoxGroup("Prefabs")] [SerializeField] private GameObject _messageBubblePrefab;
    [BoxGroup("Prefabs")] [SerializeField] private GameObject _typingBubblePrefab;
    [BoxGroup("Prefabs")] [SerializeField] private GameObject _playerTypingBubblePrefab;
    [BoxGroup("Prefabs")] [SerializeField] private GameObject _messageSpacerPrefab;

    private GameObject _typingBubble;
    private GameObject _playerTypingBubble;
    public MessageSender CurrentSender { get; protected set; }
    public MessageSender? PreviousSender { get; protected set; } = null;
    public MessageBubbleAlignment CurrentMessageAlignment { get; protected set; }
    public Sprite CurrentMessageBubble { get; protected set; }
    public Color CurrentSenderColor { get; protected set; }
    public Color CurrentTextColor { get; protected set; }
    

    [Button]
    public void StartAtFirstDay()
    {
        DialogueRunner.Stop();
        DialogueRunner.StartDialogue("Day1_HeardFromPlayer");
    }

    public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        MessageBubble messageBubble = CreateMessageBubble();
        
        if (CurrentSender is MessageSender.Me or MessageSender.System)
        {
            messageBubble.SenderName.enabled = false;
        }
        else if (PreviousSender != CurrentSender)
        {
            messageBubble.SenderName.enabled = true;
            messageBubble.SenderName.text = CurrentSender.ToString();
        }
        else if (PreviousSender == CurrentSender)
        {
            messageBubble.SenderName.enabled = false;
        }

        MessageSentCallback();
        
        PreviousSender = CurrentSender;
        messageBubble.Text.text = dialogueLine.TextWithoutCharacterName.Text;
        onDialogueLineFinished();
    }

    private void MessageSentCallback()
    {
        bool isChatActive = _contentContainer.activeInHierarchy;
        _messageSentChannel.MessageSent?.Invoke(CurrentSender, isChatActive);

        if (CurrentSender == MessageSender.Me)
        {
            _messageSentChannel.PlayerMessageSent?.Invoke(isChatActive);
        }
        else
        {
            _messageSentChannel.NpcMessageSent?.Invoke(CurrentSender, isChatActive);
        }
    }

    private void ClearOldElements()
    {
        if (_contentContainer.transform.childCount <= _maxElementCount)
        {
            return;
        }

        int difference = _contentContainer.transform.childCount - _maxElementCount;
        for (int i = 0; i < difference; i++)
        {
            Destroy(_contentContainer.transform.GetChild(i).gameObject);
        }
    }

    protected virtual void RegisterCommandHandlers()
    {
        DialogueRunner.AddCommandHandler("Me", SetSenderMe);
        DialogueRunner.AddCommandHandler("System", SetSenderSystem);
    }

    private void SetMessageBubbleSettings(MessageBubble messageBubble)
    {
        messageBubble.SetBubble(CurrentMessageBubble, CurrentSenderColor, CurrentTextColor);

        switch (CurrentMessageAlignment)
        {
            case MessageBubbleAlignment.Left:
                messageBubble.LayoutGroup.padding.left = 0;
                messageBubble.LayoutGroup.padding.right = 200;
                messageBubble.LayoutGroup.reverseArrangement = true;
                messageBubble.Background.transform.SetAsLastSibling();
                break;
            case MessageBubbleAlignment.Right:
                messageBubble.LayoutGroup.padding.left = 200;
                messageBubble.LayoutGroup.padding.right = 0;
                messageBubble.LayoutGroup.reverseArrangement = false;
                messageBubble.Background.transform.SetAsLastSibling();
                break;
        }
    }

    private MessageBubble CreateMessageBubble()
    {
        GameObject newMessageBubbleGameObject = Instantiate(_messageBubblePrefab, _messageBubblePrefab.transform.position,
            Quaternion.identity, _contentContainer.transform);
            
        //Add padding between messages if new sender
        if (PreviousSender != CurrentSender)
        {
            CreateSpacer();
        }
        
        newMessageBubbleGameObject.transform.SetAsLastSibling();
        newMessageBubbleGameObject.SetActive(true);

        var messageBubble = newMessageBubbleGameObject.GetComponent<MessageBubble>();
        SetMessageBubbleSettings(messageBubble);
        
        return messageBubble;
    }

    private void CreateSpacer()
    {
        GameObject spacer = Instantiate(_messageSpacerPrefab, _messageSpacerPrefab.transform.position,
            Quaternion.identity, _contentContainer.transform);
        spacer.SetActive(true);
        spacer.transform.SetAsLastSibling();
    }

    public void DisplayTypingBubble(bool shouldDisplay)
    {
        if (shouldDisplay)
        {
            if (_typingBubble == null)
            {
                _typingBubble = Instantiate(_typingBubblePrefab, _typingBubblePrefab.transform.position,
                    Quaternion.identity, _contentContainer.transform);
            }
            _typingBubble.transform.SetAsLastSibling();
            _typingBubble.SetActive(true);
            return;
        }

        if (_typingBubble != null)
        {
            _typingBubble.SetActive(false);
        }
    }

    // public void DisplayPlayerTypingBubble(bool shouldDisplay)
    // {
    //     if (shouldDisplay)
    //     {
    //         if (_playerTypingBubble == null)
    //         {
    //             _playerTypingBubble = Instantiate(_playerTypingBubblePrefab, _playerTypingBubblePrefab.transform.position,
    //                 Quaternion.identity, _contentContainer.transform);
    //         }
    //         _playerTypingBubble.transform.SetAsLastSibling();
    //         _playerTypingBubble.SetActive(true);
    //         return;
    //     }
    //
    //     if (_playerTypingBubble != null)
    //     {
    //         _playerTypingBubble.SetActive(false);
    //     }
    // }

    private void SetSenderSystem()
    {
        SetMessageSender(MessageSender.System);
    }

    private void SetSenderMe()
    {
        SetMessageSender(MessageSender.Me);
    }
    
    protected virtual void SetMessageSender(MessageSender sender)
    {
        CurrentSender = sender;
        switch (sender)
        {
            case MessageSender.Me:
                CurrentMessageAlignment = MessageBubbleAlignment.Right;
                CurrentMessageBubble = BubbleSettings.PlayerBubble;
                CurrentTextColor = BubbleSettings.TextColorPlayer;
                break;
            case MessageSender.System:
                CurrentMessageAlignment = MessageBubbleAlignment.Left;
                CurrentMessageBubble = BubbleSettings.SystemBubble;
                CurrentSenderColor = BubbleSettings.SenderColorSystem;
                CurrentTextColor = BubbleSettings.TextColorPlayer;
                break;
            default:
                break;
        }
    }
}

public enum MessageSender
{
    Jess,
    Megan,
    Rachel,
    Me,
    System,
    Mom,
    Boyfriend
}

public enum MessageBubbleAlignment
{
    Left,
    Right
}
