using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticsManager : MonoBehaviour
{
    public static HapticsManager instance;

    private bool isHaptics;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void EnableHaptics()
    {
        isHaptics = true;
    }

    public void DisnableHaptics()
    {
        isHaptics = false;
    }

    public static void Vibrate()
    {
        if (instance.HapticsEnabled())
            Taptic.Light();
    }

    private bool HapticsEnabled()
    {
        return isHaptics;
    }
}
