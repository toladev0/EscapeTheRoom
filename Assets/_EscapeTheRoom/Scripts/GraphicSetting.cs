using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script handles the graphics settings such as resolution, quality, and fullscreen
public class GraphicSetting : MonoBehaviour
{
    public TMPro.TMP_Dropdown resolutionDropdown;  // Dropdown for selecting resolution
    Resolution[] resolutions;                      // Array to store available screen resolutions

    private void Start()
    {
        if (resolutionDropdown != null)
        {
            resolutions = Screen.resolutions;      // Get all supported screen resolutions
            resolutionDropdown.ClearOptions();     // Clear existing options in the dropdown
            List<string> options = new List<string>();

            int CurrenResolutionIndex = 0;         // Index to store the current screen resolution
            for (int i = 0; i < resolutions.Length; i++)
            {
                // Format each resolution as "width x height"
                string option = resolutions[i].width + " x " + resolutions[i].height;
                options.Add(option);

                // Check if this resolution is the current screen resolution
                if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
                {
                    CurrenResolutionIndex = i;
                }
            }

            // Add resolution options to the dropdown
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = CurrenResolutionIndex;   // Set current resolution as selected
            resolutionDropdown.RefreshShownValue();             // Refresh to show the selected value
        }
    }

    // Change the screen resolution based on the selected dropdown index
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    // Set the quality level (Low, Medium, High, etc.)
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    // Enable or disable fullscreen mode
    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
