using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// This script controls volume settings for Master, Music, and SFX
public class SoundSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;          // The audio mixer to control volumes
    [SerializeField] private Slider MasterSlider;       // Slider for master volume
    [SerializeField] private Slider musicSlider;        // Slider for music volume
    [SerializeField] private Slider SFXSlider;          // Slider for sound effects (SFX) volume

    private void Start()
    {
        // If saved settings exist, load them; otherwise, apply current slider values
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            LoadVolume(); // Load saved volume settings
        }
        else
        {
            SetMasterVolume(); // Set and save current slider values
            SetMusicVolume();
            SetSFXVolume();
        }
    }

    // Called when the master volume slider changes
    public void SetMasterVolume()
    {
        float volume = MasterSlider.value;
        mixer.SetFloat("Master", Mathf.Log10(volume) * 20); // Convert linear value to decibel
        PlayerPrefs.SetFloat("MasterVolume", volume);       // Save value
    }

    // Called when the music volume slider changes
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        mixer.SetFloat("Music", Mathf.Log10(volume) * 20);  // Apply music volume
        PlayerPrefs.SetFloat("MusicVolume", volume);        // Save value
    }

    // Called when the SFX volume slider changes
    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        mixer.SetFloat("SFX", Mathf.Log10(volume) * 20);    // Apply SFX volume
        PlayerPrefs.SetFloat("SFXVolume", volume);          // Save value
    }

    // Load volume values from PlayerPrefs and apply them
    private void LoadVolume()
    {
        MasterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");

        SetMasterVolume(); // Apply loaded values to the mixer
        SetMusicVolume();
        SetSFXVolume();
    }
}
