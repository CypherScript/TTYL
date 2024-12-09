using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "AudioChannel", menuName = "ScriptableObjects/Audio/Channel")]
public class AudioChannel : ScriptableObject
{
    public UnityEvent<AudioType> playAudio = new UnityEvent<AudioType>();

    public void PlayAudio(AudioType type)
    {
        playAudio?.Invoke(type);
    }
}

public enum AudioType
{
    Notification = 0,
    IncomingMessage = 1,
    PlayerReply = 2
}
