using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TextCharacterSettings", menuName = "ScriptableObjects/Messaging/Settings/TextCharacterSettings")]
public class TextCharacterSettings : ScriptableObject
{
    [field: SerializeField] public Color SelectedColor { get; private set; }
    [field: SerializeField] public Color UnselectedColor { get; private set; }
    [field: SerializeField] public Color SelectedTextColor { get; private set; }
    [field: SerializeField] public Color UnselectedTextColor { get; private set; }
    [field: SerializeField] public Color TextCursorColor { get; private set; }
}
