using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PromptTextDisplay : MonoBehaviour
{
    [field: SerializeField] public int CharacterLimit { get; private set; }
    [SerializeField] private Transform _characterContainer;
    [SerializeField] private PromptChannel _promptChannel;
    [SerializeField] private PromptTextCharacter _textCharacterPrefab;
    [ShowInInspector] private List<PromptTextCharacter> _textCharacters = new List<PromptTextCharacter>();
    [SerializeField] private PhoneTextField _phoneTextField;

    public bool IsAtCharacterLimit => _textCharacters.Count >= CharacterLimit;
    private string _promptText;
    private char[] _promptTextCharArray;
    private int _characterIndex;
    private Guid _promptGuid;
    
    private void OnEnable()
    {
        _promptChannel.promptTextChanged.AddListener(SetAndShowNewPrompt);
        _phoneTextField.TextFieldUpdated += OnTextFieldUpdated;

        if (_promptChannel.PromptGuid == Guid.Empty || _promptChannel.PromptGuid != _promptGuid)
        {
            SetAndShowNewPrompt();
        }
    }

    private void OnDisable()
    {
        _promptChannel.promptTextChanged.RemoveListener(SetAndShowNewPrompt);
        _phoneTextField.TextFieldUpdated -= OnTextFieldUpdated;
    }

    [Button]
    private void SetAndShowNewPrompt()
    {
        Clear();

        _promptText = _promptChannel.Text;
        _promptTextCharArray = _promptText.ToCharArray();
        _promptGuid = _promptChannel.PromptGuid;

        foreach (char character in _promptText)
        {
            CreateNextCharacter(character.ToString());
        }
    }
    
    [Button]
    public PromptTextCharacter CreateNextCharacter()
    {
        if (IsAtCharacterLimit)
        {
            return null;
        }

        PromptTextCharacter character = Instantiate(_textCharacterPrefab, _textCharacterPrefab.transform.position,
            Quaternion.identity, _characterContainer);
        
        _textCharacters.Add(character);
        return character;
    }
    
    public void CreateNextCharacter(string text)
    {
        if (IsAtCharacterLimit)
        {
            return;
        }
        PromptTextCharacter character = CreateNextCharacter();
        character.Text = text;
    }

    [Button]
    private void HideCharacterAtIndex(int index)
    {
        if (index < 0 || index > _textCharacters.Count - 1)
        {
            return;
        }
        _textCharacters[index].Text = "";
    }

    private void RevealCharacterAtIndex(int index)
    {
        if (index < 0 || index > _textCharacters.Count - 1)
        {
            return;
        }
        _textCharacters[index].Text = _promptTextCharArray[index].ToString();
    }

    private void OnTextFieldUpdated(object sender, int targetIndex)
    {
        int difference = targetIndex - _characterIndex;

        while (difference != 0)
        {
            if (difference > 0)
            {
                _characterIndex++;
                HideCharacterAtIndex(_characterIndex);
            }
            else
            {
                RevealCharacterAtIndex(_characterIndex);
                _characterIndex--;
            }

            difference = targetIndex - _characterIndex;
        }
    }
    
    private void Clear()
    {
        _characterIndex = _phoneTextField.ActiveCharacterIndex;
        int childCount = _characterContainer.transform.childCount;
        if (childCount == 0)
        {
            return;
        }
        
        for (int i = childCount - 1; i >= 0 ; i--)
        {
            Destroy(_characterContainer.transform.GetChild(i).gameObject);
        }
        _textCharacters.Clear();
    }
}
