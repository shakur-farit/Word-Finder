using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Image soundsImage;
    [SerializeField] private Image hapticsImage;
    [SerializeField] private Image backgroundSoundImage;

    private bool soundsState;
    private bool hapticsState;
    private bool backgroundSoundState;

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

    public void BackgroundSoundButtonCallback()
    {
        backgroundSoundState = !backgroundSoundState;
        UpdateBackgroundSoundState();
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
            EnableSounds();
        else
            DisableSounds();
    }

    private void UpdateHapticsState()
    {
        if (hapticsState)
            EnableHaptics();
        else
            DisableHaptics();
    }

    private void UpdateBackgroundSoundState()
    {
        if (backgroundSoundState)
            EnableBackgroundSounds();
        else
            DisableBackgroundSounds();
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

    private void EnableBackgroundSounds()
    {
        SoundsManager.instance.PlayBackgroundSound();
        backgroundSoundImage.color = Color.white;
    }

    private void DisableBackgroundSounds()
    {
        SoundsManager.instance.StopBackgroundSound();
        backgroundSoundImage.color = Color.gray;
    }

    private void LoadStates()
    {
        soundsState = PlayerPrefs.GetInt("Sounds", 1) == 1;
        hapticsState = PlayerPrefs.GetInt("Haptics", 1) == 1;
        backgroundSoundState = PlayerPrefs.GetInt("BackgroundSound", 1) == 1;

        UpdateSoundsState();
        UpdateHapticsState();
        UpdateBackgroundSoundState();
    }

    private void SaveStates()
    {
        PlayerPrefs.SetInt("Sounds", soundsState ? 1 : 0);
        PlayerPrefs.SetInt("Haptics", hapticsState ? 1 : 0);
        PlayerPrefs.SetInt("BackgroundSound", backgroundSoundState ? 1 : 0);
    }
}
