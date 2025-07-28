using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GraphicSetting : MonoBehaviour
{
    // UI elements for user settings
    public TMP_Dropdown resolutionDropdown;   // Dropdown to choose screen resolution
    public Toggle fullscreenToggle;           // Toggle to enable/disable fullscreen
    public TMP_Dropdown qualityDropdown;      // Dropdown to select graphic quality level

    // Array of all system-supported resolutions
    private Resolution[] allResolutions;

    // List of unique screen resolutions for dropdown
    private List<Resolution> uniqueResolutions = new List<Resolution>();

    // PlayerPrefs keys to save and retrieve user settings
    private const string PREF_RES_INDEX = "ResolutionIndex";     // Key for resolution index
    private const string PREF_QUALITY_INDEX = "QualityIndex";    // Key for quality level
    private const string PREF_FULLSCREEN = "FullScreen";         // Key for fullscreen mode

    void Start()
    {
        // Set up resolution options and load saved graphic settings
        SetupResolutions();
        LoadSettings();
    }

    // Populates the resolution dropdown with unique screen resolutions
    private void SetupResolutions()
    {
        allResolutions = Screen.resolutions; // Get all available resolutions
        HashSet<string> resolutionSet = new HashSet<string>(); // To filter duplicates
        List<string> options = new List<string>(); // Dropdown text options

        int currentResolutionIndex = 0; // Index of the current screen resolution

        for (int i = 0; i < allResolutions.Length; i++)
        {
            // Create string for resolution (e.g., "1920x1080")
            string resKey = $"{allResolutions[i].width}x{allResolutions[i].height}";

            // Add only if it's not already in the list
            if (!resolutionSet.Contains(resKey))
            {
                resolutionSet.Add(resKey); // Mark as added
                uniqueResolutions.Add(allResolutions[i]); // Add to the actual list
                options.Add($"{allResolutions[i].width} x {allResolutions[i].height}"); // Add option to dropdown

                // If it's the current screen resolution, save the index
                if (allResolutions[i].width == Screen.currentResolution.width &&
                    allResolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = uniqueResolutions.Count - 1;
                }
            }
        }

        // Clear existing options in dropdown and add new ones
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);

        // Set the dropdown value to the saved resolution or default
        resolutionDropdown.onValueChanged.AddListener(SetResolution); // Link callback
        resolutionDropdown.value = PlayerPrefs.GetInt(PREF_RES_INDEX, currentResolutionIndex); // Set saved value
        resolutionDropdown.RefreshShownValue(); // Refresh UI
    }

    // Called when resolution dropdown value changes
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = uniqueResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen); // Apply resolution
        PlayerPrefs.SetInt(PREF_RES_INDEX, resolutionIndex); // Save setting
    }

    // Called when quality dropdown value changes
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex); // Apply quality level
        PlayerPrefs.SetInt(PREF_QUALITY_INDEX, qualityIndex); // Save setting
    }

    // Called when fullscreen toggle is changed
    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen; // Apply fullscreen mode
        PlayerPrefs.SetInt(PREF_FULLSCREEN, isFullscreen ? 1 : 0); // Save setting
    }

    // Loads settings from PlayerPrefs and applies them
    private void LoadSettings()
    {
        // Load and apply fullscreen mode
        bool isFullscreen = PlayerPrefs.GetInt(PREF_FULLSCREEN, Screen.fullScreen ? 1 : 0) == 1;
        Screen.fullScreen = isFullscreen;
        if (fullscreenToggle != null)
            fullscreenToggle.isOn = isFullscreen;

        // Load and apply quality setting
        int qualityIndex = PlayerPrefs.GetInt(PREF_QUALITY_INDEX, QualitySettings.GetQualityLevel());
        QualitySettings.SetQualityLevel(qualityIndex);
        if (qualityDropdown != null)
        {
            qualityDropdown.value = qualityIndex;
            qualityDropdown.RefreshShownValue(); // Update UI
        }
    }
}
