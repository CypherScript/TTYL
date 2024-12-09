using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class DebugCanvas : MonoBehaviour
{
    [SerializeField] private DialogueRunner _dialogueRunner;
    [SerializeField] private PromptChannel _promptChannel;
    [SerializeField] private FloatReference _popularity;

    [SerializeField] private TextMeshProUGUI _promptText;
    [SerializeField] private TextMeshProUGUI _popularityText;
    private Coroutine _restartDialogueCoroutine;

    private void OnEnable()
    {
        _promptChannel.promptTextChanged.AddListener(UpdatePromptText);
        _popularity.ValueChanged.AddListener(UpdatePopularityText);
        
        UpdatePromptText();
        UpdatePopularityText(_popularity.Value);
    }

    private void OnDisable()
    {
        _promptChannel.promptTextChanged.RemoveListener(UpdatePromptText);
        _popularity.ValueChanged.RemoveListener(UpdatePopularityText);
    }

    [Button]
    public void JumpToStart()
    {
        _dialogueRunner.StartDialogue("Day1_HeardFromPlayer");
    }

    [Button]
    public void RestartDialogue()
    {
        if (_restartDialogueCoroutine != null)
        {
            return;
        }
        _dialogueRunner.Stop();
        _restartDialogueCoroutine = StartCoroutine(JumpToNodeCoroutine("Day1_HeardFromPlayer"));
    }
    
    private IEnumerator JumpToNodeCoroutine(string nodeTitle)
    {
        yield return new WaitForSeconds(1f);
        _dialogueRunner.StartDialogue(nodeTitle);
        _restartDialogueCoroutine = null;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UpdatePromptText()
    {
        _promptText.text = _promptChannel.Text;
    }

    private void UpdatePopularityText(float value)
    {
        _popularityText.text = value.ToString();
    }

    [Button]
    private void StopDialogue()
    {
        _dialogueRunner.Stop();
    }
}
