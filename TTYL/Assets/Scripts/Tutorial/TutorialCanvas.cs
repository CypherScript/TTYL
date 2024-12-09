using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _highlightDpad;
    [SerializeField] private GameObject _highlightConfirm;
    [SerializeField] private GameObject _highlightLeftSelection;
    [SerializeField] private GameObject _highlightRightSelection;
    [SerializeField] private GameObject _highlightHangUp;
    [SerializeField] private GameObject _highlightPickUp;
    [SerializeField] private GameObject _highlight5Key;
    [SerializeField] private GameObject _highlight6Key;
    [SerializeField] private GameObject _highlightPoundKey;

    public void SetAllHighlightsInactive()
    {
        _highlightDpad.SetActive(false);
        _highlightConfirm.SetActive(false);
        _highlightLeftSelection.SetActive(false);
        _highlightRightSelection.SetActive(false);
        _highlightHangUp.SetActive(false);
        _highlightPickUp.SetActive(false);
        _highlight5Key.SetActive(false);
        _highlight6Key.SetActive(false);
        _highlightPoundKey.SetActive(false);
    }

    public void HighlightConfirm(bool shouldHighlight)
    {
        _highlightConfirm.SetActive(shouldHighlight);
    }

    public void HighlightLeftSelection(bool shouldHighlight)
    {
        _highlightLeftSelection.SetActive(shouldHighlight);
    }

    public void HighlightRightSelection(bool shouldHighlight)
    {
        _highlightRightSelection.SetActive(shouldHighlight);
    }

    public void HighlightHangUp(bool shouldHighlight)
    {
        _highlightHangUp.SetActive(shouldHighlight);
    }

    public void HighlightPickUp(bool shouldHighlight)
    {
        _highlightPickUp.SetActive(shouldHighlight);
    }

    public void Highlight5Key(bool shouldHighlight)
    {
        _highlight5Key.SetActive(shouldHighlight);
    }

    public void Highlight6Key(bool shouldHighlight)
    {
        _highlight6Key.SetActive(shouldHighlight);
    }

    public void HighlightPoundKey(bool shouldHighlight)
    {
        _highlightPoundKey.SetActive(shouldHighlight);
    }
}
