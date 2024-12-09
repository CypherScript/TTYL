using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomMessageDisplayer : PhoneMessageDisplayer
{
    private void Awake()
    {
        RegisterCommandHandlers();
    }

    protected override void RegisterCommandHandlers()
    {
        base.RegisterCommandHandlers();
        
        DialogueRunner.AddCommandHandler("Mom", SetSenderMom);
    }
    private void SetSenderMom()
    {
        SetMessageSender(MessageSender.Mom);
    }
    
    protected override void SetMessageSender(MessageSender sender)
    {
        base.SetMessageSender(sender);
        switch (sender)
        {
            case MessageSender.Mom:
                CurrentMessageAlignment = MessageBubbleAlignment.Left;
                CurrentMessageBubble = BubbleSettings.MomBubble;
                CurrentSenderColor = BubbleSettings.SenderColorMom;
                CurrentTextColor = BubbleSettings.TextColorNpc;
                break;
            default:
                break;
        }
    }
}
