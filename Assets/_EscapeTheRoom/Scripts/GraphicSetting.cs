using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GraphicSetting : MonoBehaviour
{
    // UI elements for resolution, fullscreen, and quality settings
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public TMP_Dropdown qualityDropdown;

    // Available screen resolutions
    private Resolution[] allResolutions;
    private List<Resolution> uniqueResolutions = new List<Resolution>();

    // PlayerPrefs keys for saving settings
    private const string PREF_RES_INDEX = "ResolutionIndex";
    private const string PREF_QUALITY_INDEX = "QualityIndex";
    private const string PREF_FULLSCREEN = "FullScreen";

    void Start()
    {
        // Initialize resolution dropdown and load saved settings
        SetupResolutions();
        LoadSettings();
    }

    // Sets up the resolution dropdown list with unique values
    private void SetupResolutions()
    {
        allResolutions = Screen.resolutions;
        HashSet<string> resolutionSet = new HashSet<string>(); // To store unique resolution strings
        List<string> options = new List<string>(); // Dropdown options

        int currentResolutionIndex = 0;

        for (int i = 0; i < allResolutions.Length; i++)
        {
            // Create a string key for comparison (e.g., "1920x1080")
            string resKey = $"{allResolutions[i].width}x{allResolutions[i].height}";

            // Add only unique resolutions
            if (!resolutionSet.Contains(resKey))
            {
                resolutionSet.Add(resKey);
                uniqueResolutions.Add(allResolutions[i]);
                options.Add($"{allResolutions[i].width} x {allResolutions[i].height}");

                // Save the index of the current screen resolution
                if (allResolutions[i].width == Screen.currentResolution.width &&
                    allResolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = uniqueResolutions.Count - 1;
                }
            }
        }

        // Update dropdown UI with resolution options
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);

        // Set the saved or current resolution
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
        resolutionDropdown.value = PlayerPrefs.GetInt(PREF_RES_INDEX, currentResolutionIndex);
        resolutionDropdown.RefreshShownValue();
    }

    // Called when user selects a resolution from dropdown
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = uniqueResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt(PREF_RES_INDEX, resolutionIndex);
    }

    // Called when user changes the quality setting
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt(PREF_QUALITY_INDEX, qualityIndex);
    }

    // Called when user toggles fullscreen mode
    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt(PREF_FULLSCREEN, isFullscreen ? 1 : 0);
    }

    // Loads saved resolution, quality, and fullscreen settings
    private void LoadSettings()
    {
        // Load fullscreen setting
        bool isFullscreen = PlayerPrefs.GetInt(PREF_FULLSCREEN, Screen.fullScreen ? 1 : 0) == 1;
        Screen.fullScreen = isFullscreen;
        if (fullscreenToggle != null)
            fullscreenToggle.isOn = isFullscreen;

        // Load quality setting
        int qualityIndex = PlayerPrefs.GetInt(PREF_QUALITY_INDEX, QualitySettings.GetQualityLevel());
        QualitySettings.SetQualityLevel(qualityIndex);
        if (qualityDropdown != null)
        {
            qualityDropdown.value = qualityIndex;
            qualityDropdown.RefreshShownValue();
        }
    }
}
