using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Day0Manager : MonoBehaviour
{
    [SerializeField] private StringReference _playerName;
    [SerializeField] private DayTransitioner _dayTransitioner;
    [SerializeField] private ReportCard _reportCard;

    private void Awake()
    {
        _reportCard.Clean();
    }

    private void OnEnable()
    {
        _playerName.ValueChanged.AddListener(OnPlayerNameSet);
    }
    

    private void OnDisable()
    {
        _playerName.ValueChanged.RemoveListener(OnPlayerNameSet);
    }

    private void OnPlayerNameSet(string playerName)
    {
        if (string.IsNullOrEmpty(playerName))
        {
            return;
        }
        _dayTransitioner.EndDay();
    }
}
