using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "MessageBubbleSettings", menuName = "ScriptableObjects/Messaging/Settings/MessageBubbleSettings")]
public class MessageBubbleSettings : ScriptableObject
{
    [BoxGroup("Text Color")] [field: SerializeField] public Color TextColorNpc { get; private set; }
    [BoxGroup("Text Color")] [field: SerializeField] public Color TextColorPlayer { get; private set; }
    [BoxGroup("Sender Color")] [field: SerializeField] public Color SenderColorSystem { get; private set; }
    [BoxGroup("Sender Color")] [field: SerializeField] public Color SenderColorMom { get; private set; }
    [BoxGroup("Sender Color")] [field: SerializeField] public Color SenderColorJess { get; private set; }
    [BoxGroup("Sender Color")] [field: SerializeField] public Color SenderColorMegan { get; private set; }
    [BoxGroup("Sender Color")] [field: SerializeField] public Color SenderColorRachel { get; private set; }
    [BoxGroup("Sender Color")] [field: SerializeField] public Color SenderColorBoyfriend { get; private set; }
    [BoxGroup("Bubble Sprites")] [field: SerializeField] public Sprite PlayerBubble { get; private set; }
    [BoxGroup("Bubble Sprites")] [field: SerializeField] public Sprite SystemBubble { get; private set; }
    [BoxGroup("Bubble Sprites")] [field: SerializeField] public Sprite MomBubble { get; private set; }
    [BoxGroup("Bubble Sprites")] [field: SerializeField] public Sprite JessBubble { get; private set; }
    [BoxGroup("Bubble Sprites")] [field: SerializeField] public Sprite MeganBubble { get; private set; }
    [BoxGroup("Bubble Sprites")] [field: SerializeField] public Sprite RachelBubble { get; private set; }
    [BoxGroup("Bubble Sprites")] [field: SerializeField] public Sprite BoyfriendBubble { get; private set; }
}
