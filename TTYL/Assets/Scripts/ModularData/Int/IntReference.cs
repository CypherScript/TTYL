using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "IntReference", menuName = "ScriptableObjects/Data/References/Int")]
public class IntReference : ScriptableObject
{
    [SerializeField] private bool _useConstant;
    [SerializeField] private int _constantValue;
    [SerializeField] private IntVariable _variable;

    public UnityEvent<int> ValueChanged;

    public int Value
    {
        get => _useConstant ? _constantValue : _variable.Value;
        set
        {
            _variable.Value = value;
            ValueChanged?.Invoke(value);
        }
    }
}
