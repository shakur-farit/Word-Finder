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

    public void HapticsButtonCallback()
    {
        hapticsState = !hapticsState;
        UpdateHapticsState();
        SaveStates();
    }

    private void UpdateSoundsState()
    {
        if (soundsState)
            DisableSounds();
        else
            EnableSounds();
    }

    private void UpdateHapticsState()
    {
        if (hapticsState)
            DisableHaptics();
        else
            EnableHaptics();
    }

    private void EnableSounds()
    {
        SoundsManager.instance.EnableSounds();
        soundsImage.color = Color.white;
    }

    private void DisableSounds()
    {
        SoundsManager.instance.DisnableSounds();
        soundsImage.color = Color.gray;
    }
    
    private void EnableHaptics()
    {
        HapticsManager.instance.EnableHaptics();
        hapticsImage.color = Color.white;
    }

    private void DisableHaptics()
    {
        HapticsManager.instance.DisnableHaptics();
        hapticsImage.color = Color.gray;
    }

    private void LoadStates()
    {
        soundsState = PlayerPrefs.GetInt("Sounds", 1) == 1;
        hapticsState = PlayerPrefs.GetInt("Haptics", 1) == 1;

        UpdateSoundsState();
        UpdateHapticsState();
    }

    private void SaveStates()
    {
        PlayerPrefs.SetInt("Sounds", soundsState ? 1 : 0);
        PlayerPrefs.SetInt("Haptics", hapticsState ? 1 : 0);
    }
}
