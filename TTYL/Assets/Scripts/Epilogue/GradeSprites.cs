using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GradeSprites", menuName = "ScriptableObjects/Epilogue/GradeSprites")]
public class GradeSprites : ScriptableObject
{
    [field: SerializeField] public Sprite S { get; private set; }
    [field: SerializeField] public Sprite A { get; private set; }
    [field: SerializeField] public Sprite B { get; private set; }
    [field: SerializeField] public Sprite C { get; private set; }
    [field: SerializeField] public Sprite F { get; private set; }
}
