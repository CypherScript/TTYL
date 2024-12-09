using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class TutorialRunner : MonoBehaviour
{
    private Effects.CoroutineInterruptToken _coroutineInterruptToken;
    private int _tutorialIndex = -1;

    public int TutorialIndex
    {
        get => _tutorialIndex;
        private set
        {
            _tutorialIndex = value;
        }
    }

    public Coroutine RunTutorialModule()
    {
        TutorialIndex++;
        StopTutorialModule();
        _coroutineInterruptToken = new Effects.CoroutineInterruptToken();
        return StartCoroutine(TutorialModuleCoroutine(_coroutineInterruptToken));
    }
    
    private IEnumerator TutorialModuleCoroutine(Effects.CoroutineInterruptToken interruptToken)
    {
        interruptToken.Start();

        while (true)
        {
            if (interruptToken.WasInterrupted)
            {
                yield break;
            }

            yield return null;
        }
        interruptToken.Complete();
    }
    
    public void StopTutorialModule()
    {
        if (_coroutineInterruptToken is {CanInterrupt: true})
        {
            _coroutineInterruptToken.Interrupt();
        }
    }
}
