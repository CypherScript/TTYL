using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Day1Manager : MonoBehaviour
{
    [SerializeField] private DisplayChannel _displayChannel;
    [SerializeField] private DialogueRunnerDayStarter _groupChatDialogueStarter;
    private bool _isMomFinished;
    private bool _isGroupChatStarted;

    private void OnEnable()
    {
        _displayChannel.ChatSelectionActive.AddListener(OnChatSelectionEnabled);
    }

    private void OnDisable()
    {
        _displayChannel.ChatSelectionActive.RemoveListener(OnChatSelectionEnabled);
    }

    [YarnCommand("MomChatComplete")]
    public void OnMomChatComplete()
    {
        _isMomFinished = true;
    }

    private void OnChatSelectionEnabled()
    {
        if (!_isMomFinished)
        {
            return;
        }

        if (_isGroupChatStarted)
        {
            return;
        }

        _isGroupChatStarted = true;
        _groupChatDialogueStarter.StartDialogue();
        _displayChannel.ChatSelectionActive.RemoveListener(OnChatSelectionEnabled);
    }
}
