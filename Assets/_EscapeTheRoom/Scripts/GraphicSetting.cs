using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class GraphicSetting : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown fullscreenDropdown;
    public TMP_Dropdown qualityDropdown;
    public TMP_Dropdown vsyncDropdown;
    public Slider fovSlider;
    public TextMeshProUGUI fovValueText;

    public CinemachineVirtualCamera virtualCamera;

    private Resolution[] allResolutions;
    private List<Resolution> uniqueResolutions = new List<Resolution>();

    private const string PREF_RES_INDEX = "ResolutionIndex";
    private const string PREF_QUALITY_INDEX = "QualityIndex";
    private const string PREF_FULLSCREEN = "FullScreen";
    private const string PREF_VSYNC = "VSync";
    private const string PREF_FOV = "FieldOfView";

    private const float FOV_MIN = 40f;
    private const float FOV_MAX = 110f;
    private const float FOV_DEFAULT = 48f;

    void Start()
    {
        SetupResolutions();
        SetupFullscreenOptions();
        SetupVSyncOptions();
        SetupFOVSlider();
        LoadSettings();
    }

    // =================== RESOLUTION ===================

    private void SetupResolutions()
    {
        allResolutions = Screen.resolutions;
        HashSet<string> resolutionSet = new HashSet<string>();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < allResolutions.Length; i++)
        {
            string resKey = $"{allResolutions[i].width}x{allResolutions[i].height}";

            if (!resolutionSet.Contains(resKey))
            {
                resolutionSet.Add(resKey);
                uniqueResolutions.Add(allResolutions[i]);
                options.Add($"{allResolutions[i].width} x {allResolutions[i].height}");

                if (allResolutions[i].width == Screen.currentResolution.width &&
                    allResolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = uniqueResolutions.Count - 1;
                }
            }
        }

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
        resolutionDropdown.value = PlayerPrefs.GetInt(PREF_RES_INDEX, currentResolutionIndex);
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = uniqueResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt(PREF_RES_INDEX, resolutionIndex);
    }

    // =================== FULLSCREEN ===================

    private void SetupFullscreenOptions()
    {
        if (fullscreenDropdown != null)
        {
            List<string> options = new List<string> { "Windowed", "Fullscreen" };
            fullscreenDropdown.ClearOptions();
            fullscreenDropdown.AddOptions(options);
            fullscreenDropdown.onValueChanged.AddListener(SetFullScreen);
        }
    }

    public void SetFullScreen(int modeIndex)
    {
        bool isFullscreen = (modeIndex == 1);
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt(PREF_FULLSCREEN, isFullscreen ? 1 : 0);
    }

    // =================== QUALITY ===================

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt(PREF_QUALITY_INDEX, qualityIndex);
    }

    // =================== V-SYNC ===================

    private void SetupVSyncOptions()
    {
        if (vsyncDropdown != null)
        {
            List<string> options = new List<string> { "Disable", "Enable" };
            vsyncDropdown.ClearOptions();
            vsyncDropdown.AddOptions(options);
            vsyncDropdown.onValueChanged.AddListener(SetVSync);
        }
    }

    public void SetVSync(int vsyncIndex)
    {
        QualitySettings.vSyncCount = vsyncIndex;
        PlayerPrefs.SetInt(PREF_VSYNC, vsyncIndex);
    }

    // =================== FOV ===================

    private void SetupFOVSlider()
    {
        if (fovSlider != null)
        {
            fovSlider.minValue = FOV_MIN;
            fovSlider.maxValue = FOV_MAX;
            fovSlider.wholeNumbers = true;

            // Prevent triggering SetFOV too early
            fovSlider.onValueChanged.RemoveAllListeners();

            // Load saved FOV or default
            float savedFOV = PlayerPrefs.GetFloat(PREF_FOV, FOV_DEFAULT);

            // Apply saved value BEFORE adding listener
            fovSlider.value = savedFOV;

            if (fovValueText != null)
                fovValueText.text = $"{savedFOV:F0}";

            if (virtualCamera != null)
                virtualCamera.m_Lens.FieldOfView = savedFOV;

            // Now add the listener
            fovSlider.onValueChanged.AddListener(SetFOV);
        }
    }

    public void SetFOV(float fovValue)
    {
        PlayerPrefs.SetFloat(PREF_FOV, fovValue);

        if (virtualCamera != null)
            virtualCamera.m_Lens.FieldOfView = fovValue;

        if (fovValueText != null)
            fovValueText.text = $"{fovValue:F0}";
    }

    // =================== LOAD SETTINGS ===================

    private void LoadSettings()
    {
        // Fullscreen
        bool isFullscreen = PlayerPrefs.GetInt(PREF_FULLSCREEN, Screen.fullScreen ? 1 : 0) == 1;
        Screen.fullScreen = isFullscreen;
        if (fullscreenDropdown != null)
        {
            fullscreenDropdown.value = isFullscreen ? 1 : 0;
            fullscreenDropdown.RefreshShownValue();
        }

        // Quality
        int qualityIndex = PlayerPrefs.GetInt(PREF_QUALITY_INDEX, QualitySettings.GetQualityLevel());
        QualitySettings.SetQualityLevel(qualityIndex);
        if (qualityDropdown != null)
        {
            qualityDropdown.value = qualityIndex;
            qualityDropdown.RefreshShownValue();
        }

        // V-Sync
        int vsyncIndex = PlayerPrefs.GetInt(PREF_VSYNC, QualitySettings.vSyncCount);
        QualitySettings.vSyncCount = vsyncIndex;
        if (vsyncDropdown != null)
        {
            vsyncDropdown.value = vsyncIndex;
            vsyncDropdown.RefreshShownValue();
        }

        // FOV already handled in SetupFOVSlider()
    }
}
