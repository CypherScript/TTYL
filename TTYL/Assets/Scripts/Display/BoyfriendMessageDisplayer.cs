using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoyfriendMessageDisplayer : PhoneMessageDisplayer
{
    private void Awake()
    {
        RegisterCommandHandlers();
    }

    protected override void RegisterCommandHandlers()
    {
        base.RegisterCommandHandlers();
        
        DialogueRunner.AddCommandHandler("Boyfriend", SetSenderBoyfriend);
    }

    private void SetSenderBoyfriend()
    {
        SetMessageSender(MessageSender.Boyfriend);
    }
    
    protected override void SetMessageSender(MessageSender sender)
    {
        base.SetMessageSender(sender);
        switch (sender)
        {
            case MessageSender.Boyfriend:
                CurrentMessageAlignment = MessageBubbleAlignment.Left;
                CurrentMessageBubble = BubbleSettings.BoyfriendBubble;
                CurrentSenderColor = BubbleSettings.SenderColorBoyfriend;
                CurrentTextColor = BubbleSettings.TextColorNpc;
                break;
            default:
                break;
        }
    }
}
