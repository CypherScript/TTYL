using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class VerticalMenu : MenuBase
{
    [Button]
    protected override void OnUpKeyPressed()
    {
        base.OnUpKeyPressed();

        if (ButtonIndex == 0)
        {
            SetButtonActive(ButtonIndex);
            return;
        }

        ButtonIndex--;
    }

    [Button]
    protected override void OnDownKeyPressed()
    {
        base.OnDownKeyPressed();

        if (ButtonIndex == ButtonGameObjects.Length - 1)
        {
            SetButtonActive(ButtonIndex);
            return;
        }

        ButtonIndex++;
    }
}
