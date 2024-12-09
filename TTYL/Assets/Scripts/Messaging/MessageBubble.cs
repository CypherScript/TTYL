using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageBubble : MonoBehaviour
{
    [field: SerializeField] public Image Background { get; private set; }
    [field: SerializeField] public TextMeshProUGUI SenderName { get; private set; }
    [field: SerializeField] public TextMeshProUGUI Text { get; private set; }
    [field: SerializeField] public HorizontalLayoutGroup LayoutGroup { get; private set; }

    public void SetBubble(Sprite bubbleSprite, Color senderColor, Color textColor)
    {
        Background.sprite = bubbleSprite;
        SenderName.color = senderColor;
        Text.color = textColor;
    }
}
