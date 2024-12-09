using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OnScreenButtonSettings", menuName = "ScriptableObjects/Menu/Settings/OnScreenButtonSettings")]
public class OnScreenButtonSettings : ScriptableObject
{
    [field: SerializeField] public Color SelectedColor { get; private set; }
    [field: SerializeField] public Color UnselectedColor { get; private set; }
    [field: SerializeField] public Color SelectedTextColor { get; private set; }
    [field: SerializeField] public Color UnselectedTextColor { get; private set; }
}
