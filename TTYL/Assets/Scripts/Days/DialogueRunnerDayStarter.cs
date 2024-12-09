using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Yarn.Unity;

public class DialogueRunnerDayStarter : MonoBehaviour
{
    [SerializeField] private DayChannel _dayChannel;
    [SerializeField] private DialogueRunner _dialogueRunner;
    [SerializeField] private string _startNode;
    [SerializeField] private bool _shouldStartAtStartOfDay;

    private void OnEnable()
    {
        _dayChannel.DayStarted.AddListener(OnDayStarted);
    }

    private void OnDisable()
    {
        _dayChannel.DayStarted.RemoveListener(OnDayStarted);
    }

    [Button]
    public void StartDialogue()
    {
        _dialogueRunner.StartDialogue(_startNode);
    }
    
    [YarnCommand("StartDialogue")]
    public void StartDialogue(float delay)
    {
        StartCoroutine(StartDialogueAfterDelay(delay));
    }

    private void OnDayStarted()
    {
        if (!_shouldStartAtStartOfDay)
        {
            return;
        }
        StartDialogue();
    }

    private IEnumerator StartDialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _dialogueRunner.StartDialogue(_startNode);
    }
}
