using UnityEngine;

public class PauseMenuManagerment : MonoBehaviour
{
    // Reference to the UI panel that will be shown when the game is paused
    public GameObject pausePanel;

    void Update()
    {
        // Check if Escape key (keyboard) is pressed
        bool keyboardPause = Input.GetKeyDown(KeyCode.Escape);

        // Check if controller "Pause" button is pressed (e.g., mapped to joystick button 7 or 9)
        //bool controllerPause = Input.GetButtonDown(controllerPauseButton);

        if (keyboardPause)
        {
            // Show the pause panel (and pause logic can be added here too)
            pausePanel.SetActive(true);
        }
    }
}
