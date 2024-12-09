using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioSettings", menuName = "ScriptableObjects/Audio/Settings")]
public class AudioSettings : ScriptableObject
{
    [field: SerializeField] public AudioClip BGMClip { get; private set; }
    [field: SerializeField] public AudioClip NotificationClip { get; private set; }
    [field: SerializeField] public AudioClip IncomingMessageClip { get; private set; }
    [field: SerializeField] public AudioClip RingtoneClip { get; private set; }
    [field: SerializeField] public AudioClip GoodReplyClip { get; private set; }
    [field: SerializeField] public AudioClip BadReplyClip { get; private set; }
    [field: SerializeField] public AudioClip KeypadPressedClip { get; private set; }
    [field: SerializeField] public AudioClip MomPhoneCallClip { get; private set; }
    [field: SerializeField] public AudioClip BoyfriendPhoneCallClip { get; private set; }
    [field: SerializeField] public AudioClip TypewriterClip { get; private set; }
    [field: SerializeField] public AudioClip GradeRevealClip { get; private set; }
    [field: SerializeField] public AudioClip PopularityHit { get; private set; }

    [field: SerializeField] public float BGMVolume { get; private set; }
    [field: SerializeField] public float NotificationVolume { get; private set; }
    [field: SerializeField] public float IncomingMessageVolume { get; private set; }
    [field: SerializeField] public float RingtoneVolume { get; private set; }
    [field: SerializeField] public float PromptSubmittedVolume { get; private set; }
    [field: SerializeField] public float KeypadPressedVolume { get; private set; }
    [field: SerializeField] public float MomPhoneCallVolume { get; private set; }
    [field: SerializeField] public float BoyfriendPhoneCallVolume { get; private set; }
    [field: SerializeField] public float TypewriterVolume { get; private set; }
    [field: SerializeField] public float GradeRevealVolume { get; private set; }
    [field: SerializeField] public float PopularityHitVolume { get; private set; }
}
