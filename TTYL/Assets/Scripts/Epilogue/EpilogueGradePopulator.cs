using System;
using System.Collections;
using System.Collections.Generic;
using Epilogue;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Yarn.Unity;

public class EpilogueGradePopulator : MonoBehaviour
{
    [SerializeField] private GradeSprites _gradeSprites;
    [SerializeField] private ReportCard _reportCard;
    [SerializeField] private Image _gradeImage;
    [SerializeField] private EpilogueChannel _epilogueChannel;
    [SerializeField] private MessageSender _gradeType;
    
    private void Awake()
    {
        SetGrade();
        _gradeImage.gameObject.SetActive(false);
    }

    private void SetGrade()
    {
        Grade grade = _gradeType switch
        {
            MessageSender.Mom => _reportCard.GetMomGrade(),
            MessageSender.Boyfriend => _reportCard.GetBoyfriendGrade(),
            _ => _reportCard.GetPopularityGrade()
        };

        _gradeImage.sprite = grade switch
        {
            Grade.S => _gradeSprites.S,
            Grade.A => _gradeSprites.A,
            Grade.B => _gradeSprites.B,
            Grade.C => _gradeSprites.C,
            Grade.F => _gradeSprites.F,
            _ => _gradeImage.sprite
        };
    }

    [YarnCommand("ShowGrade")]
    public void ShowGrade()
    {
        _gradeImage.gameObject.SetActive(true);
        _epilogueChannel.GradeRevealed?.Invoke();
    }
}
