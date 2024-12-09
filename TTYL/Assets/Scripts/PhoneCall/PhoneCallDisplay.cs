using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhoneCallDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _containerGO = null;
    [SerializeField] private GameObject _phoneImageGO = null;
    [SerializeField] private KeypadChannel _keypadChannel = null;
    [SerializeField] private PhoneCallChannel _phoneCallChannel = null;
    [SerializeField] private PlatformChannel _platformChannel = null;
    [SerializeField] private TextMeshProUGUI _incomingCallText = null;
    [SerializeField] private TextMeshProUGUI _callerNameText = null;
    [SerializeField] private GameObject _pauseGO = null;
    [SerializeField] private float _speed = 100f;
    [SerializeField] private float _shakeAmount = 5f;
    [SerializeField] private float _shakeTime = 1f;
    [SerializeField] private float _shakeFrequency = 1f;

    private float _callDuration = 0;
    private string _nameText = "";
    private Coroutine _hangUpIfNotAnswered = null;
    private Coroutine _hangUpIfAnswered = null;
    private Coroutine _phoneShake = null;
    private Coroutine _timeInCall = null;

    private void Awake()
    {
        _phoneCallChannel.PhoneCall.AddListener(OnCallReceived);
        _phoneCallChannel.PhoneCallEnded.AddListener(OnCallFinish);

        _keypadChannel.PickupButtonPressed.AddListener(OnPickupButtonPressed);
        _keypadChannel.HangUpButtonPressed.AddListener(OnHangupButtonPressed);
        _keypadChannel.GamePaused?.AddListener(OnGamePaused);

        _pauseGO.SetActive(false);
        _containerGO.SetActive(false);
    }

    private void OnDestroy()
    {
        _phoneCallChannel.PhoneCall.RemoveListener(OnCallReceived);
        _phoneCallChannel.PhoneCallEnded.RemoveListener(OnCallFinish);

        _keypadChannel.PickupButtonPressed.RemoveListener(OnPickupButtonPressed);
        _keypadChannel.HangUpButtonPressed.RemoveListener(OnHangupButtonPressed);
        _keypadChannel.GamePaused?.RemoveListener(OnGamePaused);

        if (_hangUpIfNotAnswered != null) StopCoroutine(_hangUpIfNotAnswered);
        if (_hangUpIfAnswered != null) StopCoroutine(_hangUpIfAnswered);
        if (_phoneShake != null) StopCoroutine(_phoneShake);
        if (_timeInCall != null) StopCoroutine(_timeInCall);
    }

    private void OnPickupButtonPressed()
    {
        if (!_phoneCallChannel.IsPhoneCallActive) return;

        if (_hangUpIfNotAnswered != null) StopCoroutine(_hangUpIfNotAnswered);
        if (_hangUpIfAnswered != null) StopCoroutine(_hangUpIfAnswered);
        if (_phoneShake != null) StopCoroutine(_phoneShake);
        if (_timeInCall != null) StopCoroutine(_timeInCall);

        _hangUpIfAnswered = StartCoroutine(HangPhoneAfterAnswering());
        _timeInCall = StartCoroutine(TimeInCall());
        _phoneCallChannel.AnswerPhoneCall();
    }

    private void OnHangupButtonPressed()
    {
        if (!_phoneCallChannel.IsPhoneCallActive) return;

        if (_hangUpIfNotAnswered != null) StopCoroutine(_hangUpIfNotAnswered);
        if (_hangUpIfAnswered != null) StopCoroutine(_hangUpIfAnswered);
        if (_phoneShake != null) StopCoroutine(_phoneShake);
        if (_timeInCall != null) StopCoroutine(_timeInCall);

        _phoneCallChannel.EndPhoneCall(false);
    }

    private void OnCallReceived()
    {
        _containerGO.SetActive(true);

        if(_hangUpIfAnswered == null)
            _hangUpIfNotAnswered = StartCoroutine(HangPhoneIfNotAnswered());

        if (_phoneShake == null)
            _phoneShake = StartCoroutine(ShakePhoneImage());

        if (_phoneCallChannel.CurrentCaller == Characters.Boyfriend)
            _nameText = "Needy Boyfriend";
        else
            _nameText = "Mom";

        _callerNameText.text = _nameText;

        VibratePhone();
    }

    private void OnCallFinish(bool wasAnswered)
    {
        if (_hangUpIfNotAnswered != null) StopCoroutine(_hangUpIfNotAnswered);
        if (_hangUpIfAnswered != null) StopCoroutine(_hangUpIfAnswered);
        if (_phoneShake != null) StopCoroutine(_phoneShake);
        if (_timeInCall != null) StopCoroutine(_timeInCall);

        _incomingCallText.text = "INCOMING CALL";

        _containerGO.SetActive(false);
    }

    private void VibratePhone()
    {
        #if !PLATFORM_WEBGL
                Handheld.Vibrate();
        #endif 
    }

    private void OnGamePaused(bool isPaused)
    {
        if (isPaused)
            _pauseGO.SetActive(true);
        else
            _pauseGO.SetActive(false);
    }

    private IEnumerator HangPhoneIfNotAnswered()
    {
        yield return new WaitForSeconds(_phoneCallChannel.TimeToHangup);
        _phoneCallChannel.EndPhoneCall(false);
    }

    private IEnumerator HangPhoneAfterAnswering()
    {
        yield return new WaitForSeconds(_phoneCallChannel.TimeToHangupAfterAnswering);
        _phoneCallChannel.EndPhoneCall(true);
    }

    private IEnumerator ShakePhoneImage()
    {
        RectTransform rect = _phoneImageGO.GetComponent<RectTransform>();

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            Vector2 newPos = Vector2.zero;
            newPos.x = Mathf.Sin(Time.time * _speed) * _shakeAmount;
            newPos.y = rect.anchoredPosition.y;
            rect.anchoredPosition = newPos;

            yield return null;
        }

        yield return new WaitForSeconds(_shakeFrequency);

        _phoneShake = StartCoroutine(ShakePhoneImage());
    }

    private IEnumerator TimeInCall()
    {
        _incomingCallText.text = _callerNameText.text;
        _callDuration = 0;
        var time = TimeSpan.FromSeconds(_callDuration);
        _callerNameText.text = string.Format("{0:00}:{1:00}", time.Minutes, time.Seconds);

        while (_phoneCallChannel.IsPhoneCallActive)
        {
            yield return new WaitForSeconds(1);
            _callDuration += 1;
            time = TimeSpan.FromSeconds(_callDuration);
            _callerNameText.text = string.Format("{0:00}:{1:00}", time.Minutes, time.Seconds);
        }
    }
}
