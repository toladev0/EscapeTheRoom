using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManagerment : MonoBehaviour
{
    // Reference to the UI panel that will be shown when the game is paused
    public GameObject pausePanel;

    void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Show the pause panel
            pausePanel.SetActive(true);
        }
    }
}
