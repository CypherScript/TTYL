using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Yarn.Unity;

public class PromptRunner : MonoBehaviour
{
    [SerializeField] private ChatType _chatType;
    [SerializeField] private DialogueRunner _dialogueRunner;
    [SerializeField] private PromptChannel _promptChannel;
    [SerializeField] private PhoneMessageDisplayer _phoneMessageDisplayer;
    [SerializeField] private PromptSettings _promptSettings;
    private Coroutine _promptCoroutine;
    private Effects.CoroutineInterruptToken _coroutineInterruptToken;
    private PromptResult _promptResult;
    private string _variableStringSuffix;

    private void Awake()
    {
        _variableStringSuffix = "_" + _chatType;
        _promptChannel.ClearPrompt();
    }

    private void Start()
    {
        _promptChannel.responseSubmitted.AddListener(OnPromptSubmission);
    }

    private void OnDestroy()
    {
        _promptChannel.responseSubmitted.RemoveListener(OnPromptSubmission);
    }

    [YarnCommand("RunPrompt")]
    public Coroutine RunPrompt(float time)
    {
        StopPrompt();
        _coroutineInterruptToken = new Effects.CoroutineInterruptToken();
        _promptCoroutine = StartCoroutine(Prompt(time, _coroutineInterruptToken));
        return _promptCoroutine;
    }

    public void StopPrompt()
    {
        if (_coroutineInterruptToken is {CanInterrupt: true})
        {
            _coroutineInterruptToken.Interrupt();
        }
    }

    public void ClearPrompt()
    {
        _promptChannel.ClearPrompt();
    }

    private IEnumerator Prompt(float time, Effects.CoroutineInterruptToken interruptToken)
    {
        _promptChannel.StartPrompt();
        interruptToken.Start();
        float elapsedTime = 0;
        
        //Set the prompt result to NoReply. This is the default if left unchanged when time expires
        _promptResult = PromptResult.NoReply;
        _dialogueRunner.VariableStorage.SetValue("$promptResults" + _variableStringSuffix, (int)_promptResult);

        while (true)
        {
            if (elapsedTime >= time)
            {
                break;
            }

            if (interruptToken.WasInterrupted)
            {
                _promptChannel.EndPrompt(_promptResult);
                _phoneMessageDisplayer.DisplayTypingBubble(false);
                yield break;
            }

            if (elapsedTime / time > _promptSettings.DisplayTypingBubblePercent)
            {
                _phoneMessageDisplayer.DisplayTypingBubble(true);
            }
            elapsedTime += Time.deltaTime;
            
            yield return null;
        }

        _phoneMessageDisplayer.DisplayTypingBubble(false);
        _promptChannel.EndPrompt(_promptResult);
        interruptToken.Complete();
    }

    [YarnCommand("SetPromptText")]
    public void SetPromptText(string text)
    {
        _promptChannel.Text = text;
    }

    private void OnPromptSubmission(string submission)
    {
        string promptToUpper = _promptChannel.Text.ToUpper();
        string submissionToUpper = submission.ToUpper();
        float similarity = Utilities.CalculatePercentSimilarity(promptToUpper, submissionToUpper);

        _promptResult = (PromptResult)(similarity >= _promptSettings.MinResponseAccuracy ? 0 : 1);
        
        //Set the prompt result to GoodReply or BadReply based on response accuracy
        _dialogueRunner.VariableStorage.SetValue("$promptResults" + _variableStringSuffix, (int)_promptResult);
        //Set the response text to reflect the user input response
        _dialogueRunner.VariableStorage.SetValue("$responseText" + _variableStringSuffix, submission);

        StopPrompt();
    }

    [Button]
    private void DebugGoodReply()
    {
        _coroutineInterruptToken?.Interrupt();
        
        //Set the prompt result to GoodReply
        _dialogueRunner.VariableStorage.SetValue("$promptResults" + _variableStringSuffix, 0);
    }

    [Button]
    private void DebugBadReply()
    {
        _coroutineInterruptToken?.Interrupt();
        
        //Set the prompt result to GoodReply
        _dialogueRunner.VariableStorage.SetValue("$promptResults" + _variableStringSuffix, 1);
    }

    [Button]
    private void RestartScene()
    {
        SceneManager.LoadScene(0);
    }
}

public enum ChatType
{
    Group,
    Mom,
    Boyfriend
}
