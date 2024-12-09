using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

public class PhoneTextField : MonoBehaviour
{
    [field: SerializeField] public int CharacterLimit { get; private set; }
    [SerializeField] private TextCharacter _textCharacterPrefab;
    [SerializeField] private GameObject _textCursorPrefab;
    [SerializeField] private Transform _characterContainer;
    [ShowInInspector] private List<TextCharacter> _textCharacters = new List<TextCharacter>();

    private TextCharacter _activeCharacter;
    private GameObject _textCursorGameObject;
    public event EventHandler<int> TextFieldUpdated;
    
    
    public TextCharacter ActiveCharacter
    {
        get => _activeCharacter;
        set
        {
            _activeCharacter = value;

            if (value != null)
            {
                SetCharacterSelected(value); 
            }
        }
    }

    public int ActiveCharacterIndex
    {
        get
        {
            if (!_activeCharacter)
            {
                return -1;
            }
            
            return _textCharacters.IndexOf(_activeCharacter);
        }
    } 
    
    [ShowInInspector] private string _text;
    public string Text
    {
        get => _text;
        set
        {
            _text = value;
        }
    }

    public bool IsAtCharacterLimit => Text.Length >= CharacterLimit;

    private void Awake()
    {
        Clear();
    }

    [Button]
    public void CreateNextCharacter()
    {
        if (IsAtCharacterLimit)
        {
            return;
        }
        if (ActiveCharacter != null)
        {
            ActiveCharacter.SetUnSelected();
        }
        ActiveCharacter = Instantiate(_textCharacterPrefab, _textCharacterPrefab.transform.position,
            Quaternion.identity, _characterContainer);
        _textCharacters.Add(ActiveCharacter);
        TextFieldUpdated?.Invoke(this, ActiveCharacterIndex);
    }
    
    public void CreateNextCharacter(string text)
    {
        if (IsAtCharacterLimit)
        {
            return;
        }
        CreateNextCharacter();
        ActiveCharacter.Text = text;
    }

    private void SetCharacterSelected(TextCharacter character)
    {
        character.SetSelected();
        if (_textCursorGameObject)
        {
            Destroy(_textCursorGameObject);
            _textCursorGameObject = null;
        }
    }

    public void DeleteLastCharacter()
    {
        if (_textCharacters.Count == 0)
        {
            return;
        }

        var characterToDelete = ActiveCharacter;

        ActiveCharacter = _textCharacters.Count < 2 ? null : _textCharacters[^2];
        
        if (characterToDelete != null)
        {
            _textCharacters.Remove(characterToDelete);
            Destroy(characterToDelete.gameObject);
        }
        ShowTextCursor();
        TextFieldUpdated?.Invoke(this, ActiveCharacterIndex);
    }

    [Button]
    public void Clear()
    {
        _text = "";
        DestroyAllChildren();
        CreateTextCursor();
        TextFieldUpdated?.Invoke(this, ActiveCharacterIndex);
    }

    private void DestroyAllChildren()
    {
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

    private void CreateTextCursor()
    {
        _textCursorGameObject = Instantiate(_textCursorPrefab, _textCursorPrefab.transform.position,
            Quaternion.identity, _characterContainer);
    }

    public void ShowTextCursor()
    {
        if (_textCursorGameObject)
        {
            return;
        }
        
        CreateTextCursor();
        _textCursorGameObject.transform.SetAsLastSibling();
        
        if (ActiveCharacter != null)
        {
            ActiveCharacter.SetUnSelected();
        }
    }
}

public class TextFieldEventArgs : EventArgs
{
    public string Text { get; private set; }
    public bool IsAdded { get; private set; }
    public int Index { get; private set; }

    public TextFieldEventArgs(string text, bool isAdded, int index)
    {
        Text = text;
        IsAdded = isAdded;
        Index = index;
    }
}
