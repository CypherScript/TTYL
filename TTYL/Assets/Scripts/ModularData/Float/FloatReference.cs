using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "FloatReference", menuName = "ScriptableObjects/Data/References/Float")]
public class FloatReference : ScriptableObject
{
    [SerializeField] private bool _useConstant;
    [SerializeField] private float _constantValue;
    [SerializeField] private FloatVariable _variable;

    public UnityEvent<float> ValueChanged;

    public float Value
    {
        get => _useConstant ? _constantValue : _variable.Value;
        set
        {
            _variable.Value = value;
            ValueChanged?.Invoke(value);
        }
    }
}
