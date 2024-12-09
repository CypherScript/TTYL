using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Yarn.Unity;

public class DayTransitioner : MonoBehaviour
{
    [SerializeField] private IntReference _currentDay;
    [SerializeField] private DaySceneReference _daySceneReference;
    [SerializeField] private DayChannel _dayChannel;
    [SerializeField] private TitleCard _startDayTitleCard;
    [SerializeField] private TitleCard _endDayTitleCard;
    [SerializeField] private DayTransitionSettings _transitionSettings;

    [SerializeField] private bool _shouldSetDay;
    [SerializeField] private int _dayToSet;
    [SerializeField] private bool _debugSkipTransitions;

    private void Awake()
    {
        if (_shouldSetDay)
        {
            _currentDay.Value = _dayToSet;
        }
    }

    private void Start()
    {
#if UNITY_EDITOR
        if (_debugSkipTransitions)
        {
            Destroy(_startDayTitleCard.gameObject);
            _dayChannel.DayStarted?.Invoke();
            return;
        }
#endif
        
        _dayChannel.NewDayTransitionStarted?.Invoke();
        Invoke(nameof(StartDay), _transitionSettings.StartDayTitlePauseDuration);
    }
    
    private void GoToNextDay()
    {
        _currentDay.Value++;
        if (_currentDay.Value < 1)
        {
            Debug.LogError($"Current day is a value of less than 1. Current Day = {_currentDay.Value}");
            return;
        }

        if (_currentDay.Value > _daySceneReference.SceneNames.Length)
        {
            Debug.LogError($"Current day is greater than the number of days. Current Day = {_currentDay.Value}");
            return;
        }
        
        LoadSceneAtIndex(_currentDay.Value);
    }

    private void StartDay()
    {
        _startDayTitleCard.FadeInText(_transitionSettings.StartDayFadeDuration, 1, () =>
        {
            Debug.Log("Days Text faded in");
            StartCoroutine(StartDayTransition());
        });
    }

    [Button]
    [YarnCommand("EndDay")]
    public void EndDay()
    {
        _dayChannel.DayEnded?.Invoke();
        _endDayTitleCard.gameObject.SetActive(true);
        _endDayTitleCard.FadeIn(_transitionSettings.EndDayFadeDuration, GoToNextDay);
    }

    [Button]
    private void LoadSceneAtIndex(int index)
    {
        SceneManager.LoadScene(_daySceneReference.SceneNames[index]);
    }

    private IEnumerator StartDayTransition()
    {
        yield return new WaitForSeconds(_transitionSettings.StartDaySubtitlePauseDuration);
        
        _startDayTitleCard.FadeOut(_transitionSettings.StartDayFadeDuration, () =>
        {
            _dayChannel.DayStarted?.Invoke();
        });
    }
}
