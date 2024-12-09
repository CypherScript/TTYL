using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ChatSelectionMenuChannel", menuName = "ScriptableObjects/Menu/Channels/ChatSelectionMenuChannel")]
public class ChatSelectionMenuChannel : ScriptableObject
{
    public UnityEvent GroupChatButtonPressed = new UnityEvent();
    public UnityEvent MomChatButtonPressed = new UnityEvent();
    public UnityEvent BoyfriendChatButtonPressed = new UnityEvent();
}
