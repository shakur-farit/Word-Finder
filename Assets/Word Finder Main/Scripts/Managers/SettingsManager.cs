using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Image soundsImage;
    [SerializeField] private Image hapticsImage;

    private bool soundsState;
    private bool hapticsState;

    private void Start()
    {
        LoadStates();
    }

    public void SoundsButtonCallback()
    {
        soundsState = !soundsState;
        UpdateSoundsState();
        SaveStates();
    }

    private void UpdateSoundsState()
    {
        if (soundsState)
            DisableSounds();
        else
            EnableSounds();
    }

    private void EnableSounds()
    {
        //SoundsManager.EnableSouhnds()
        soundsImage.color = Color.white;
    }

    private void DisableSounds()
    {
        //SoundsManager.EnableSouhnds()
        soundsImage.color = Color.gray;
    }
    
    private void LoadStates()
    {
        soundsState = PlayerPrefs.GetInt("Sounds", 1) == 1;
        hapticsState = PlayerPrefs.GetInt("Haptics", 1) == 1;

        UpdateSoundsState();
    }

    private void SaveStates()
    {
        PlayerPrefs.SetInt("Sounds", soundsState ? 1 : 0);
        PlayerPrefs.SetInt("Haptics", hapticsState ? 1 : 0);
    }
}
