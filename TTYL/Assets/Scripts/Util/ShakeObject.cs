using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class ShakeObject : MonoBehaviour
{
    [SerializeField] private PhoneCallChannel _phoneCallChannel = null;
    [SerializeField] private PlatformChannel _platformChannel = null;
    [SerializeField] private MessageSentChannel _messageSentChannel = null;
    [SerializeField] private FloatReference _popularity = null;
    [SerializeField] private float _speed = 100f; 
    [SerializeField] private float _shakeAmount = 5f;
    [SerializeField] private float _shakeTime = 1f;
    [SerializeField] private float _shakeFrequency = 1f;
    private RectTransform _rectTransform = null;
    private Coroutine _phoneShake = null;
    private Coroutine _phoneShakeOnPopularityHit = null;
    private Coroutine _phoneShakeOnNotificationReceived = null;

    private void Awake()
    {
        #if !UNITY_EDITOR
                if (_platformChannel.Platform != RuntimePlatform.WebGLPlayer) return;
        #endif
        _rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
#if !UNITY_EDITOR
                if (_platformChannel.Platform != RuntimePlatform.WebGLPlayer) return;
#endif

        _popularity.ValueChanged.AddListener(OnPopularityChanged);

        _messageSentChannel.TriggerHapticFeedback.AddListener(OnNotificationReceived);

        _phoneCallChannel.PhoneCall.AddListener(ShakePhone);
        _phoneCallChannel.PhoneCallAnswered.AddListener(StopShake);
        _phoneCallChannel.PhoneCallEnded.AddListener(StopShake);
    }

    private void OnDisable()
    {
        #if !UNITY_EDITOR
                if (_platformChannel.Platform != RuntimePlatform.WebGLPlayer) return;
        #endif

        _popularity.ValueChanged.RemoveListener(OnPopularityChanged);

        _messageSentChannel.TriggerHapticFeedback.RemoveListener(OnNotificationReceived);

        _phoneCallChannel.PhoneCall.RemoveListener(ShakePhone);
        _phoneCallChannel.PhoneCallAnswered.RemoveListener(StopShake);
        _phoneCallChannel.PhoneCallEnded.RemoveListener(StopShake);
    }

    private void OnNotificationReceived()
    {
        if (_phoneShakeOnNotificationReceived != null) StopCoroutine(_phoneShakeOnNotificationReceived);

        _phoneShakeOnNotificationReceived = StartCoroutine(PopularityHitPhoneShake());
    }

    private void OnPopularityChanged(float value)
    {
        if (_phoneShakeOnPopularityHit != null) StopCoroutine(_phoneShakeOnPopularityHit);

        switch(_popularity.Value)
        {
            case 0:
            case 1:
            case 2:
                _phoneShakeOnPopularityHit = StartCoroutine(PopularityHitPhoneShake());
            break;
        }
    }

    private void ShakePhone()
    {
        _phoneShake = StartCoroutine(PhoneShake());
    }

    private void StopShake(Characters caller)
    {
        if(_phoneShake != null) StopCoroutine(_phoneShake);
    }

    private void StopShake(bool wasAnswered)
    {
        if (_phoneShake != null) StopCoroutine(_phoneShake);
    }

    private IEnumerator PhoneShake()
    {
        float t = 0;
        while(t < _shakeTime)
        {
            t += Time.deltaTime;
            Vector2 newPos = Vector2.zero;
            newPos.x = Mathf.Sin(Time.time * _speed) * _shakeAmount;
            newPos.y = _rectTransform.anchoredPosition.y;
            _rectTransform.anchoredPosition = newPos;

            yield return null;
        }

        yield return new WaitForSeconds(_shakeFrequency);

        _phoneShake = StartCoroutine(PhoneShake());
    }

    private IEnumerator PopularityHitPhoneShake()
    {
        float t = 0;
        while (t < 0.5f)
        {
            t += Time.deltaTime;
            Vector2 newPos = Vector2.zero;
            newPos.x = Mathf.Sin(Time.time * _speed) * _shakeAmount;
            newPos.y = _rectTransform.anchoredPosition.y;
            _rectTransform.anchoredPosition = newPos;

            yield return null;
        }

        yield return new WaitForSeconds(_shakeFrequency);
    }
}
