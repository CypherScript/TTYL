using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "TypewriterChannel", menuName = "ScriptableObjects/Typewriter/Channel")]
public class TypewriterChannel : ScriptableObject
{
    public UnityEvent CharacterTyped = new UnityEvent();
}
