using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Day2Manager : MonoBehaviour
{
    [SerializeField] private DialogueRunnerDayStarter _boyfriendDialogueStarter;

    [YarnCommand("StartBoyfriendChat")]
    public void StartBoyfriendChat()
    {
        _boyfriendDialogueStarter.StartDialogue();
    }
}
