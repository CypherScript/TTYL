using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeverSleep : MonoBehaviour
{
    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

}
