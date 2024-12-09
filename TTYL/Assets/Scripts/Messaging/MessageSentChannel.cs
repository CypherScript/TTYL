using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "MessageSentChannel", menuName = "ScriptableObjects/Messaging/Channels/MessageSentChannel")]
public class MessageSentChannel : ScriptableObject
{
    public UnityEvent<MessageSender, bool> MessageSent { get; private set; } = new UnityEvent<MessageSender, bool>();
    public UnityEvent<bool> PlayerMessageSent { get; private set; } = new UnityEvent<bool>();
    public UnityEvent<MessageSender, bool> NpcMessageSent { get; private set; } = new UnityEvent<MessageSender, bool>();
    public UnityEvent TriggerHapticFeedback { get; private set; } = new UnityEvent();
}
