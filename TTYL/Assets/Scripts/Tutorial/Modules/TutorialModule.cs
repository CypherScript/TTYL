using System;
using System.Collections;
using System.Collections.Generic;
using Tutorial;
using UnityEngine;

public class TutorialModule : MonoBehaviour
{
    [SerializeField] protected PhoneTextField TextField;
    [SerializeField] protected TutorialCanvas TutorialCanvas;
    [SerializeField] protected TutorialChannel TutorialChannel;

    protected virtual void Awake()
    {
        enabled = false;
    }

    protected virtual void Start()
    {
        TutorialChannel.ModuleStarted.AddListener(OnModuleStarted);
        TutorialChannel.ModuleEnded.AddListener(OnModuleEnded);
    }

    protected virtual void OnDestroy()
    {
        TutorialChannel.ModuleStarted.RemoveListener(OnModuleStarted);
        TutorialChannel.ModuleEnded.RemoveListener(OnModuleEnded);
    }

    protected virtual void OnModuleStarted()
    {
        enabled = true;
    }

    protected virtual void OnModuleEnded()
    {
        enabled = false;
    }
    
}
