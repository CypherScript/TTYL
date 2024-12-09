using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "DisplayChannel", menuName = "ScriptableObjects/Display/Channel")]
public class DisplayChannel : ScriptableObject
{
    public UnityEvent ChatSelectionActive = new UnityEvent();
    public UnityEvent GroupChatActive = new UnityEvent();
    public UnityEvent MomChatActive = new UnityEvent();
    public UnityEvent BoyfriendChatActive = new UnityEvent();
    public UnityEvent GameOverActive = new UnityEvent();
}
