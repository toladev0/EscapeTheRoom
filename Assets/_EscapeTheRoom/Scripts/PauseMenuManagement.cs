using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManagerment : MonoBehaviour
{
    // Reference to the UI panel that will be shown when the game is paused
    public GameObject pausePanel;

    // You can customize this if you're using a different input system
    [Header("Controller Support")]
    public string controllerPauseButton = "Pause"; // Must match the button name in Input Manager or Input System

    void Update()
    {
        // Check if Escape key (keyboard) is pressed
        bool keyboardPause = Input.GetKeyDown(KeyCode.Escape);

        // Check if controller "Pause" button is pressed (e.g., mapped to joystick button 7 or 9)
        bool controllerPause = Input.GetButtonDown(controllerPauseButton);

        if (keyboardPause || controllerPause)
        {
            // Show the pause panel (and pause logic can be added here too)
            pausePanel.SetActive(true);
        }
    }
}
