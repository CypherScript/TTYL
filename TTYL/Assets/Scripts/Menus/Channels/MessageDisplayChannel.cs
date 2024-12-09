using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "MessageDisplayChannel", menuName = "ScriptableObjects/Messaging/Channels/MessageDisplayChannel")]
public class MessageDisplayChannel : ScriptableObject
{
    public UnityEvent BackButtonPressed = new UnityEvent();
}
