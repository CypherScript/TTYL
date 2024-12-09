using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PromptTextCharacter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public string Text
    {
        get => _text.text;
        set => _text.text = value;
    }
}
