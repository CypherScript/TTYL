using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class GroupChatMessageDisplayer : PhoneMessageDisplayer
{
    private void Awake()
    {
        RegisterCommandHandlers();
    }

    protected override void RegisterCommandHandlers()
    {
        base.RegisterCommandHandlers();
        
        DialogueRunner.AddCommandHandler("Jess", SetSenderJess);
        DialogueRunner.AddCommandHandler("Megan", SetSenderMegan);
        DialogueRunner.AddCommandHandler("Rachel", SetSenderRachel);
    }

    private void SetSenderJess()
    {
        SetMessageSender(MessageSender.Jess);
    }

    private void SetSenderMegan()
    {
        SetMessageSender(MessageSender.Megan);
    }

    private void SetSenderRachel()
    {
        SetMessageSender(MessageSender.Rachel);
    }
    
    protected override void SetMessageSender(MessageSender sender)
    {
        base.SetMessageSender(sender);
        switch (sender)
        {
            case MessageSender.Jess:
                CurrentMessageAlignment = MessageBubbleAlignment.Left;
                CurrentMessageBubble = BubbleSettings.JessBubble;
                CurrentSenderColor = BubbleSettings.SenderColorJess;
                CurrentTextColor = BubbleSettings.TextColorNpc;
                break;
            case MessageSender.Megan:
                CurrentMessageAlignment = MessageBubbleAlignment.Left;
                CurrentMessageBubble = BubbleSettings.MeganBubble;
                CurrentSenderColor = BubbleSettings.SenderColorMegan;
                CurrentTextColor = BubbleSettings.TextColorNpc;
                break;
            case MessageSender.Rachel:
                CurrentMessageAlignment = MessageBubbleAlignment.Left;
                CurrentMessageBubble = BubbleSettings.RachelBubble;
                CurrentSenderColor = BubbleSettings.SenderColorRachel;
                CurrentTextColor = BubbleSettings.TextColorNpc;
                break;
            default:
                break;
        }
    }
}
