using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameOverMenuChannel", menuName = "ScriptableObjects/Menu/Channels/GameOverMenuChannel")]
public class GameOverMenuChannel : ScriptableObject
{
    [field: SerializeField] public string GameOverMessage { get; private set; }
    public UnityEvent RestartButtonPressed = new UnityEvent();

    public void SetGameOverMessage(string message)
    {
        GameOverMessage = message;
    }
}
