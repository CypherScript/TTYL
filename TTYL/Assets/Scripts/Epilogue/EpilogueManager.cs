using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class EpilogueManager : MonoBehaviour
{
    [SerializeField] private DialogueRunner _dialogueRunner;
    [SerializeField] private ReportCard _reportCard;
    [SerializeField] private TitleCard _groupGradeCard;
    [SerializeField] private TitleCard _momGradeCard;
    [SerializeField] private TitleCard _boyfriendGradeCard;

    [YarnCommand("SetGrades")]
    public void SetGrades()
    {
        _dialogueRunner.VariableStorage.SetValue("$popularityGrade", (int)_reportCard.GetPopularityGrade());
        _dialogueRunner.VariableStorage.SetValue("$momGrade", (int)_reportCard.GetMomGrade());
        _dialogueRunner.VariableStorage.SetValue("$boyfriendGrade", (int)_reportCard.GetBoyfriendGrade());
    }
    
    [YarnCommand("SetGroupGradeCardActive")]
    public void SetGroupGradeCardActive()
    {
        _groupGradeCard.gameObject.SetActive(true);
        _groupGradeCard.FadeInText(1, 0, null);
    }

    [YarnCommand("SetMomGradeCardActive")]
    public void SetMomGradeCardActive()
    {
        _momGradeCard.gameObject.SetActive(true);
        _momGradeCard.FadeInText(1, 0, null);
    }
    
    [YarnCommand("SetBoyfriendGradeCardActive")]
    public void SetBoyfriendGradeCardActive()
    {
        _boyfriendGradeCard.gameObject.SetActive(true);
        _boyfriendGradeCard.FadeInText(1, 0, null);
    }

    [YarnCommand("FadeOutGroup")]
    public void FadeOutGroup()
    {
        _groupGradeCard.FadeOut(1, null);
    }

    [YarnCommand("FadeOutMom")]
    public void FadeOutMom()
    {
        _momGradeCard.FadeOut(1, null);
    }

    [YarnCommand("FadeOutBoyfriend")]
    public void FadeOutBoyfriend()
    {
        _boyfriendGradeCard.FadeOut(1, null);
    }

    [YarnCommand("LoadCreditsScene")]
    public void LoadCreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }
}
