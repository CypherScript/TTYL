using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using Yarn.Unity;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private TitleCard _tittleCard = null;
    [SerializeField] private DayTransitionSettings _transitionSettings = null;
    [SerializeField] private DialogueRunner _gameOverDialogueRunner = null;
    [SerializeField] private DataStorage _storedData = null;
    [SerializeField] private Button _continue = null;
    [SerializeField] private Button _restart = null;

    private void Awake()
    {
        //_gameOverDialogueRunner.AddCommandHandler("HandleButtons", HandleButtons);
        _tittleCard.FadeInText(_transitionSettings.StartDayFadeDuration, 0, HandleButtons);
    }

    private void HandleButtons()
    {
        CanvasGroup continueCanvasGroup = _continue.gameObject.GetComponent<CanvasGroup>();
        CanvasGroup restartCanvasGroup = _restart.gameObject.GetComponent<CanvasGroup>();

        continueCanvasGroup.DOFade(1, 1);
        restartCanvasGroup.DOFade(1, 1);

        _continue.onClick.AddListener(Continue);
        _restart.onClick.AddListener(Restart);
    }

    private void Continue()
    {
        _gameOverDialogueRunner.Stop();
        SceneManager.LoadScene(_storedData.CurrentScene);
    }

    private void Restart()
    {
        _gameOverDialogueRunner.Stop();
        SceneManager.LoadScene("Day0");
    }
}
