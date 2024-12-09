using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatSelectionMenu : VerticalMenu
{
    [SerializeField] private ChatSelectionMenuChannel _channel;

    public void OnGroupChatButtonPressed()
    {
        _channel.GroupChatButtonPressed?.Invoke();
    }

    public void OnMomChatButtonPressed()
    {
        _channel.MomChatButtonPressed?.Invoke();
    }

    public void OnBoyfriendChatButtonPressed()
    {
        _channel.BoyfriendChatButtonPressed?.Invoke();
    }
}
