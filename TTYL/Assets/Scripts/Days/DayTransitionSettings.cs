using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DayTransitionSettings", menuName = "ScriptableObjects/Day/TransitionSettings")]
public class DayTransitionSettings : ScriptableObject
{
    [field: SerializeField] public float StartDayFadeDuration { get; private set; }
    [field: SerializeField] public float StartDayTitlePauseDuration { get; private set; }
    [field: SerializeField] public float StartDaySubtitlePauseDuration { get; private set; }
    [field: SerializeField] public float EndDayFadeDuration { get; private set; }
}
