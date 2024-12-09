using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "PopularitySettings", menuName = "ScriptableObjects/Popularity/Settings")]
public class PopularitySettings : ScriptableObject
{
    [BoxGroup("Base Values")] [field: SerializeField] public float MinPopularity { get; private set; }
    [BoxGroup("Base Values")] [field: SerializeField] public float StartingPopularity { get; private set; }
    [BoxGroup("Damage Values")] [field: SerializeField] public float NoReplyDamage { get; private set; }
    [BoxGroup("Damage Values")] [field: SerializeField] public float InaccurateReplyDamage { get; private set; }
}
