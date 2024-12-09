using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlatformChannel", menuName = "ScriptableObjects/Platform/PlatformChannel")]
public class PlatformChannel : ScriptableObject
{
    public RuntimePlatform Platform { get; set; }
}
