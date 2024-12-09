using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using Yarn.Unity;

public class PrologueManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private DialogueRunner _dialogueRunner;
    [SerializeField] private GameObject _startGameButton;
    [SerializeField] private DialogueTypewriter _dialogueTypewriter;
    [SerializeField] private TitleCard _titleCard;
    [SerializeField] private float _buffer;

    private float _elapsedTime;
    private bool _isTimerStarted;

    private void Awake()
    {
        _videoPlayer.loopPointReached += OnCutsceneComplete;
    }

    private void Start()
    {
#if !PLATFORM_WEBGL
        StartGame();
#endif
    }

    private void Update()
    {
        if (!_isTimerStarted)
        {
            return;
        }
        _elapsedTime += Time.unscaledDeltaTime;
    }

    [Button]
    private void PlayCutscene()
    {
        _videoPlayer.Play();
    }

    private void OnCutsceneComplete(VideoPlayer source)
    {
        Debug.Log($"Cutscene complete");
        SceneManager.LoadScene("Day0");
    }

    [YarnCommand("SetTypewriterSpeed")]
    public void SetTypewriterSpeed(float lettersPerSecond)
    {
        _dialogueTypewriter.LettersPerSecond = lettersPerSecond;
    }

    [YarnCommand("DialogueComplete")]
    public void OnDialogueComplete()
    {
        _titleCard.FadeOut(1f, PlayCutscene);
    }

    public void SkipIntro()
    {
        if (_elapsedTime < _buffer)
        {
            return;
        }
        
        SceneManager.LoadScene("Day0");
    }

    public void StartGame()
    {
        _dialogueRunner.StartDialogue("Prologue");
        Destroy(_startGameButton);
        _isTimerStarted = true;
    }
}
