using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "DayChannel", menuName = "ScriptableObjects/Day/Channels/DayChannel")]
public class DayChannel : ScriptableObject
{
    public UnityEvent NewDayTransitionStarted = new UnityEvent();
    public UnityEvent DayStarted = new UnityEvent();
    public UnityEvent DayEnded = new UnityEvent();
}
