using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialHighlightSettings", menuName = "ScriptableObjects/Tutorial/Settings/TutorialHighlightSettings")]
public class TutorialHighlightSettings : ScriptableObject
{
    [field: SerializeField] public float MinAlpha { get; private set; }
    [field: SerializeField] public float MaxAlpha { get; private set; }
    [field: SerializeField] public Color Color { get; private set; }
    [field: SerializeField] public float PulseTime { get; private set; }
    [field: SerializeField] public float RevealTime { get; private set; }
}
