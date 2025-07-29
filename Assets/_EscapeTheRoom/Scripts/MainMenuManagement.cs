using UnityEngine;

// This script manages the main menu actions
public class MainMenuManagement : MonoBehaviour
{
    private void Start()
    {
        // Unlock and show the cursor for menu navigation
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Ensure the game is not paused (in case we came from a paused state)
        Time.timeScale = 1f;
    }

    // This function is called when the Exit button is clicked
    public void Exit()
    {
        Application.Quit(); // Closes the game application
        // Note: This only works in a built application, not in the Unity editor.
    }
}
