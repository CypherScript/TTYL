using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptCleaner : MonoBehaviour
{
    [SerializeField] private List<PromptChannel> _promptChannels;

    private void Awake()
    {
        CleanPromptChannels();
    }

    private void CleanPromptChannels()
    {
        if (_promptChannels.Count <= 0)
        {
            return;
        }

        foreach (PromptChannel promptChannel in _promptChannels)
        {
            promptChannel.ClearPrompt();
        }
    }
}
