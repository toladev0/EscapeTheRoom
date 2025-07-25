using UnityEngine;
using System.Collections;

// This script shows a level intro panel that fades in, stays visible, then fades out
public class LevelIntroPanel : MonoBehaviour
{
    public GameObject panel;              // UI panel showing the level name (e.g., "Level 1")
    public float fadeDuration = 1f;       // How long the fade in/out takes
    public float displayDuration = 3f;    // How long the panel stays fully visible

    private CanvasGroup canvasGroup;      // Used to control the panel's transparency

    void Start()
    {
        if (panel != null)
        {
            // Try to get the CanvasGroup component, or add one if it doesn't exist
            canvasGroup = panel.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = panel.AddComponent<CanvasGroup>();
            }

            // Start showing the panel with fade effects
            StartCoroutine(ShowPanel());
        }
    }

    // Coroutine to show the panel with fade-in and fade-out
    IEnumerator ShowPanel()
    {
        panel.SetActive(true);           // Show the panel
        canvasGroup.alpha = 0f;          // Start fully transparent

        // Fade in the panel
        yield return StartCoroutine(FadeCanvasGroup(canvasGroup, 0f, 1f, fadeDuration));

        // Wait while the panel is fully visible
        yield return new WaitForSeconds(displayDuration);

        // Fade out the panel
        yield return StartCoroutine(FadeCanvasGroup(canvasGroup, 1f, 0f, fadeDuration));

        panel.SetActive(false);          // Hide the panel after fading out
    }

    // Coroutine to fade a CanvasGroup from one alpha value to another over time
    IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, elapsed / duration); // Interpolate alpha
            yield return null;
        }
        cg.alpha = end; // Ensure final alpha is set
    }
}
