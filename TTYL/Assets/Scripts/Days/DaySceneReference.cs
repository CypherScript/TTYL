using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DaySceneReference", menuName = "ScriptableObjects/Day/SceneReference")]
public class DaySceneReference : ScriptableObject
{
    [field: SerializeField] public string[] SceneNames { get; private set; }
}
