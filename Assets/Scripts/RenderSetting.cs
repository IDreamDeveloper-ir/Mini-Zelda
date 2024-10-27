using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderSetting : MonoBehaviour
{
    private void Start()
    {
        if (Screen.currentResolution.refreshRateRatio.value >= 50)
        {
            Application.targetFrameRate = 60;
        }
        else
        {
            Application.targetFrameRate = 30;
        }

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
