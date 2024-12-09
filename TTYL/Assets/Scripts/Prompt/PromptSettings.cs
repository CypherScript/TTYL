using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PromptSettings", menuName = "ScriptableObjects/Prompt/Settings")]
public class PromptSettings : ScriptableObject
{
    [field: SerializeField] public float MinResponseAccuracy { get; private set; }
    [field: SerializeField] public float DisplayTypingBubblePercent { get; private set; }
}
