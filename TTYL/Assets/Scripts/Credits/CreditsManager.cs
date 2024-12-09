using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] private TitleCard _creditsTitleCard = null;
    [SerializeField] private DayTransitionSettings _transitionSettings = null;
    [SerializeField] private Animator _anim = null;

    private void Awake()
    {
        _creditsTitleCard.FadeInText(_transitionSettings.StartDayFadeDuration, 0, null);
        RollCredits();
    }

    private void RollCredits()
    {
        _anim.enabled = true;
    }
}
