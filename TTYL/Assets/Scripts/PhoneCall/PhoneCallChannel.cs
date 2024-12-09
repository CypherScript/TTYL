using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PhoneCallChannel", menuName = "ScriptableObjects/PhoneCall/Channel")]
public class PhoneCallChannel : ScriptableObject
{
    public float TimeToHangup = 10f;
    public float TimeToHangupAfterAnswering = 12f;
    public UnityEvent PhoneCall = new UnityEvent();
    public UnityEvent<Characters> PhoneCallAnswered = new UnityEvent<Characters>();
    public UnityEvent<bool> PhoneCallEnded = new UnityEvent<bool>();

    public Characters CurrentCaller { get; private set; }

    public bool IsPhoneCallActive = false;
    public bool IsCallActive = false;

    private void Awake()
    {
        IsPhoneCallActive = false;
        IsCallActive = false;
    }

    public void StartPhoneCall(Characters caller)
    {
        if (IsPhoneCallActive) return;

        IsPhoneCallActive = true;
        CurrentCaller = caller;
        PhoneCall?.Invoke();
    }

    public void AnswerPhoneCall()
    {
        IsCallActive = true;
        PhoneCallAnswered?.Invoke(CurrentCaller);
    }

    public void EndPhoneCall(bool wasAnswered)
    {
        if (!IsPhoneCallActive) return;

        IsCallActive = false;
        IsPhoneCallActive = false;
        PhoneCallEnded?.Invoke(wasAnswered);
    }
}

public enum Characters
{
    Mom,
    Boyfriend
}
