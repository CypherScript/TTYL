using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "StringReference", menuName = "ScriptableObjects/Data/References/String")]
public class StringReference : ScriptableObject
{
    [SerializeField] private bool _useConstant;
    [SerializeField] private string _constantValue;
    [SerializeField] private StringVariable _variable;
    
    public UnityEvent<string> ValueChanged;

    public string Value
    {
        get => _useConstant ? _constantValue : _variable.Value;
        set
        {
            _variable.Value = value;
            ValueChanged?.Invoke(value);
        }
    }
}
