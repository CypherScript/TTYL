using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class GameOverMenu : VerticalMenu
{
    [SerializeField] private GameOverMenuChannel _gameOverMenuChannel;
    [SerializeField] private TextMeshProUGUI _gameOverText;

    protected override void OnEnable()
    {
        base.OnEnable();
    }
    
    protected override void OnUpKeyPressed()
    {
        base.OnUpKeyPressed();

        Debug.Log("Up Key Pressed");
    }
    
    protected override void OnDownKeyPressed()
    {
        base.OnDownKeyPressed();

        Debug.Log("Down Key Pressed");
    }

    public void OnRestartButtonPressed()
    {
        _gameOverMenuChannel.RestartButtonPressed?.Invoke();
    }

    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }
}
