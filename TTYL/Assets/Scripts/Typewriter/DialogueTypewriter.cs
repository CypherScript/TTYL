using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Yarn.Unity;

public class DialogueTypewriter : DialogueViewBase
{
    [SerializeField] private DialogueRunner _dialogueRunner;
    [SerializeField] private TypewriterChannel _typewriterChannel;
    [SerializeField] private TextMeshProUGUI _text;
    public float LettersPerSecond = 7f;
    private Coroutine _typewriterCoroutine;
    
    
    public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        if (_typewriterCoroutine != null)
        {
            StopCoroutine(_typewriterCoroutine);
        }
        
        _text.text = dialogueLine.TextWithoutCharacterName.Text;

        _typewriterCoroutine = StartCoroutine(TypeWriterText());

        IEnumerator TypeWriterText() 
        {
            yield return StartCoroutine(Effects.Typewriter(_text, LettersPerSecond, OnCharacterTyped));
            _typewriterCoroutine = null;
            onDialogueLineFinished();
        }
    }

    private void OnCharacterTyped()
    {
        //Do not count spaces
        if (_text.text[_text.maxVisibleCharacters - 1].Equals(' '))
        {
            return;
        }
        _typewriterChannel.CharacterTyped?.Invoke();
    }
}
