using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Cutscene : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;
    private void Awake()
    {
        _videoPlayer.url = System.IO.Path.Combine (Application.streamingAssetsPath,"PhoneBreakCutscene.mp4");
    }
}
