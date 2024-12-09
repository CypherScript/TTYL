using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Epilogue;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSettings _audioSettings = null;
    [SerializeField] private PromptChannel _groupChatChannel = null;
    [SerializeField] private PromptChannel _momChatChannel = null;
    [SerializeField] private PromptChannel _boyfriendChatChannel = null;
    [SerializeField] private MessageSentChannel _messageSentChannel = null;
    [SerializeField] private KeypadChannel _keypadChannel = null;
    [SerializeField] private PhoneCallChannel _phoneCallChannel = null;
    [SerializeField] private DayChannel _dayChannel = null;
    [SerializeField] private TypewriterChannel _typewriterChannel;
    [SerializeField] private EpilogueChannel _epilogueChannel;
    [SerializeField] private FloatReference _popularity;

    private List<GameObject> _audioSourcesPool = new();
    private List<GameObject> _pausedAudioSources = new();
    private AudioSource _sfxAudioSource = null;
    private int _poolElementsToAdd = 10;

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
            _audioSourcesPool.Add(transform.GetChild(i).gameObject);
    }

    private void OnEnable()
    {
        _groupChatChannel.promptEnded.AddListener(OnPromptSubmitted);
        _momChatChannel.promptEnded.AddListener(OnPromptSubmitted);
        _boyfriendChatChannel.promptEnded.AddListener(OnPromptSubmitted);
        _messageSentChannel.NpcMessageSent.AddListener(OnMessageReceived);
        _messageSentChannel.MessageSent.AddListener(OnNotificationReceived);
        _keypadChannel.KeypadButtonPressed.AddListener(OnKeypadPressed);
        _keypadChannel.GamePaused.AddListener(OnGamePaused);
        _keypadChannel.GamePaused.AddListener(OnGameUnpaused);
        _phoneCallChannel.PhoneCall.AddListener(OnPlayerCalled);
        _phoneCallChannel.PhoneCallAnswered.AddListener(OnCallAnswered);
        _phoneCallChannel.PhoneCallEnded.AddListener(OnCallHangup);
        _dayChannel.NewDayTransitionStarted.AddListener(OnNewDayTransitionStarted);
        _typewriterChannel.CharacterTyped.AddListener(OnCharacterTyped);
        _epilogueChannel.GradeRevealed.AddListener(OnGradeRevealed);
        _popularity.ValueChanged.AddListener(OnPopularityHit);
    }

    private void OnDisable()
    {
        _groupChatChannel.promptEnded.RemoveListener(OnPromptSubmitted);
        _momChatChannel.promptEnded.RemoveListener(OnPromptSubmitted);
        _boyfriendChatChannel.promptEnded.RemoveListener(OnPromptSubmitted);
        _messageSentChannel.NpcMessageSent.RemoveListener(OnMessageReceived);
        _messageSentChannel.MessageSent.AddListener(OnNotificationReceived);
        _keypadChannel.KeypadButtonPressed.RemoveListener(OnKeypadPressed);
        _keypadChannel.GamePaused.RemoveListener(OnGamePaused);
        _keypadChannel.GamePaused.RemoveListener(OnGameUnpaused);
        _phoneCallChannel.PhoneCall.RemoveListener(OnPlayerCalled);
        _phoneCallChannel.PhoneCallAnswered.RemoveListener(OnCallAnswered);
        _phoneCallChannel.PhoneCallEnded.RemoveListener(OnCallHangup);
        _dayChannel.NewDayTransitionStarted.RemoveListener(OnNewDayTransitionStarted);
        _typewriterChannel.CharacterTyped.RemoveListener(OnCharacterTyped);
        _epilogueChannel.GradeRevealed.RemoveListener(OnGradeRevealed);
        _popularity.ValueChanged.RemoveListener(OnPopularityHit);
    }

    private void OnPopularityHit(float value)
    {
        if (_popularity.Value == 3) return;

        PlaySfxAudio(_audioSettings.PopularityHit, _audioSettings.PopularityHitVolume);
    }

    private void OnGradeRevealed()
    {
        PlaySfxAudio(_audioSettings.GradeRevealClip, _audioSettings.GradeRevealVolume);
    }
    
    private void OnCharacterTyped()
    {
        PlaySfxAudio(_audioSettings.TypewriterClip, _audioSettings.TypewriterVolume);
    }
    
    private void OnNewDayTransitionStarted()
    {
        PlaySfxAudio(_audioSettings.IncomingMessageClip, _audioSettings.IncomingMessageVolume);
    }

    private void OnPromptSubmitted(PromptResult result)
    {
        if (result == PromptResult.GoodReply)
        {
            PlaySfxAudio(_audioSettings.GoodReplyClip, _audioSettings.PromptSubmittedVolume);
            return;
        }
        
        PlaySfxAudio(_audioSettings.BadReplyClip, _audioSettings.PromptSubmittedVolume);
    }

    private void OnMessageReceived(MessageSender npc, bool isChatActive)
    {
        if (!isChatActive) return;

        PlaySfxAudio(_audioSettings.IncomingMessageClip, _audioSettings.IncomingMessageVolume);
    }

    private void OnPlayerCalled()
    {
        PlaySfxAudio(_audioSettings.RingtoneClip, _audioSettings.RingtoneVolume, 1, true);
    }

    private void OnCallAnswered(Characters caller)
    {
        AudioSource audioSource;

        List<AudioSource> audioSources = new List<AudioSource>();
        audioSources = transform.GetComponentsInChildren<AudioSource>().ToList();
        audioSource = audioSources.Find(a => a.clip == _audioSettings.RingtoneClip);

        if (audioSource == null) return;

        if (_sfxAudioSource.clip != null)
        {
            if (_sfxAudioSource.clip == audioSource.clip)
                _sfxAudioSource = null;
        }

        audioSource.Stop();
        audioSource.clip = null;
        audioSource.gameObject.SetActive(false);

        switch (caller)
        {
            case Characters.Mom:
                PlaySfxAudio(_audioSettings.MomPhoneCallClip, _audioSettings.MomPhoneCallVolume);
                break;
            case Characters.Boyfriend:
                PlaySfxAudio(_audioSettings.BoyfriendPhoneCallClip, _audioSettings.BoyfriendPhoneCallVolume);
                break;
        }
    }

    private void OnCallHangup(bool wasAnswered)
    {
        AudioSource audioSource;

        foreach (Transform child in transform)
        {
            if (!child.gameObject.activeSelf) continue;

            audioSource = child.gameObject.GetComponent<AudioSource>();

            if (audioSource == null) continue;

            if (audioSource.clip == _audioSettings.RingtoneClip ||
                audioSource.clip == _audioSettings.MomPhoneCallClip || 
                audioSource.clip == _audioSettings.BoyfriendPhoneCallClip)
            {
                audioSource.Stop();
                audioSource.clip = null;
                child.gameObject.SetActive(false);
                break;
            }
        }
    }

    private void OnNotificationReceived(MessageSender sender, bool isChatActive)
    {
        if (isChatActive) return;

        PlaySfxAudio(_audioSettings.NotificationClip, _audioSettings.NotificationVolume);
    }

    private void OnKeypadPressed()
    {
        PlaySfxAudio(_audioSettings.KeypadPressedClip, _audioSettings.KeypadPressedVolume);
    }

    private void PlaySfxAudio(AudioClip clip, float volume, float pitch = 1, bool isLoop = false)
    {
        if (clip == null)
        {
            UnityEngine.Debug.Log("AUDIO CLIP IS NULL!!!");
            return;
        }

        GameObject audioSourceGO = GetAvailablePoolElement();

        if (audioSourceGO == null)
        {
            UnityEngine.Debug.Log("AUDIO SOURCE GO NULL!!");
            return;
        }

        audioSourceGO.SetActive(true);
        _sfxAudioSource = audioSourceGO.GetComponent<AudioSource>();

        if (_sfxAudioSource == null)
        {
            UnityEngine.Debug.Log("AUDIO SOURCE NULL!!");
            return;
        }

        _sfxAudioSource.clip = clip;
        _sfxAudioSource.loop = isLoop;
        _sfxAudioSource.volume = volume;
        _sfxAudioSource.pitch = pitch;

        _sfxAudioSource.Play();

        if (!isLoop)
            StartCoroutine(DisableAfterPlaying(audioSourceGO));
    }

    private GameObject GetAvailablePoolElement()
    {
        for (int i = 0; i < _audioSourcesPool.Count; i++)
        {
            if (i == _audioSourcesPool.Count - 1)
                AddPoolElements();

            if (!_audioSourcesPool[i].activeSelf)
                return _audioSourcesPool[i];
        }

        return null;
    }

    private void AddPoolElements()
    {
        for (int i = 0; i < _poolElementsToAdd; i++)
        {
            GameObject newPoolElement = new GameObject("SFX_AudioSource");
            newPoolElement.transform.parent = transform;
            newPoolElement.AddComponent<AudioSource>();
            newPoolElement.SetActive(false);
            _audioSourcesPool.Add(newPoolElement);
        }
    }

    private IEnumerator DisableAfterPlaying(GameObject go)
    {
        AudioSource audioSource = go.GetComponent<AudioSource>();
        yield return new WaitForSeconds(audioSource.clip.length);

        _sfxAudioSource = null;
        go.SetActive(false);
    }

    private void OnGamePaused(bool isPaused)
    {
        if (!isPaused) return;

        _pausedAudioSources = new List<GameObject>();

        foreach (GameObject child in _audioSourcesPool)
        {
            if (child.activeSelf)
            {
                AudioSource source = child.GetComponent<AudioSource>();

                if (source == null) continue;
                source.Pause();
                _pausedAudioSources.Add(child);
            }
        }
    }

    private void OnGameUnpaused(bool isPaused)
    {
        if (isPaused || _pausedAudioSources == null) return;

        for (int i = 0; i < _pausedAudioSources.Count; i++)
        {
            AudioSource source = _pausedAudioSources[i].GetComponent<AudioSource>();

            if (source == null) continue;
            source.UnPause();
        }

        _pausedAudioSources.Clear();
    }
}
