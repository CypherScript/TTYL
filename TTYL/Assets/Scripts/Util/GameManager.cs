using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FloatReference _popularity;
    [SerializeField] private List<DialogueRunner> _dialogueRunners;
    [SerializeField] private List<PromptRunner> _promptRunners;
    [SerializeField] private PhoneDisplayCanvasController _displayCanvasController;
    [FormerlySerializedAs("_gameManagerChannel")] [SerializeField] private ReportCard reportCard;
    [SerializeField] private DayChannel _dayChannel;
    [SerializeField] private TitleCard _gameOverTitleCard;

    [BoxGroup("Menu Channels")] [SerializeField]
    private GameOverMenuChannel _gameOverMenuChannel;

    private void OnEnable()
    {
        _popularity.ValueChanged.AddListener(OnPopularityUpdated);
        _gameOverMenuChannel.RestartButtonPressed.AddListener(RestartGame);
        _dayChannel.DayEnded.AddListener(StoreGroupChatScore);
    }

    private void OnDisable()
    {
        _popularity.ValueChanged.RemoveListener(OnPopularityUpdated);
        _gameOverMenuChannel.RestartButtonPressed.RemoveListener(RestartGame);
        _dayChannel.DayEnded.RemoveListener(StoreGroupChatScore);
    }

    private void OnPopularityUpdated(float value)
    {
        if (value > 0)
        {
            return;
        }
        
        GameOver();
    }

    [Button]
    private void GameOver()
    {
        if(_gameOverTitleCard == null)
        {
            Debug.Log("TITTLE CARD IS NULL!!");
            return;
        }

        StopAllProcesses();

        StartCoroutine(LoadGameOver());

        IEnumerator LoadGameOver()
        {
            yield return new WaitForSeconds(2);
            _gameOverTitleCard.gameObject.SetActive(true);
            _gameOverTitleCard.FadeIn(3, LoadGameOverScene);
        }
    }

    private void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOver");
    }

    private void StoreGroupChatScore()
    {
        reportCard.GroupChatScore += (int) _popularity.Value;
        Debug.Log("GROUP CHAT SCORE: " +  reportCard.GroupChatScore);  
    }

    private void StopAllProcesses()
    {
        foreach (DialogueRunner dialogueRunner in _dialogueRunners)
        {
            dialogueRunner.Stop();
        }

        foreach (PromptRunner promptRunner in _promptRunners)
        {
            promptRunner.StopPrompt();
            promptRunner.ClearPrompt();
        }
    }

    public void RestartGame()
    {
        StopAllProcesses();
        SceneManager.LoadScene("Day0");
    }
}
