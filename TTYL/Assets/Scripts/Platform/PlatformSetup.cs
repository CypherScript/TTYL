using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSetup : MonoBehaviour
{
    [SerializeField] PlatformSettings _mobileSettings = null;
    [SerializeField] PlatformSettings _webGlSettings = null;
    [SerializeField] PlatformChannel _platformChannel = null;

    private RuntimePlatform _currentPlatform = RuntimePlatform.WebGLPlayer;

    private void Awake()
    {
        _currentPlatform = Application.platform;

        _platformChannel.Platform = _currentPlatform;

        switch(_currentPlatform)
        {
            case RuntimePlatform.WebGLPlayer:
                //LOAD WEBGL SETTINGS
                break;
            case RuntimePlatform.Android:
            case RuntimePlatform.IPhonePlayer:
                //LOAD MOBILE SETTINGS
                break;
        }
    }
}
