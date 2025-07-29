using UnityEngine;

// This script hides and locks the mouse cursor, useful for FPS-style games
public class HideLockMouse : MonoBehaviour
{
    // Reference to the pause panel UI
    public GameObject pausePanel;

    private void Start()
    {
        // Ensure time scale is reset when scene loads
        Time.timeScale = 1f;
    }

    private void Update()
    {
        // If the pause panel is active, unlock and show the mouse cursor
        if (pausePanel.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;  // Allow free movement
            Cursor.visible = true;                   // Show the cursor
            Time.timeScale = 0f;                     // Pause the game
        }
        else
        {
            // Otherwise, lock and hide the mouse cursor for gameplay
            Cursor.lockState = CursorLockMode.Locked; // Keep cursor centered
            Cursor.visible = false;                   // Hide the cursor
            Time.timeScale = 1f;                      // Resume the game
        }
    }
}
