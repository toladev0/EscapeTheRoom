using UnityEngine;

// This script toggles the visibility and lock state of the cursor based on the GameObject's active state.
public class CursorToggle : MonoBehaviour
{
    void Update()
    {
        // Check if the GameObject this script is attached to is NOT active in the scene
        if (!this.gameObject.activeSelf)
        {
            Cursor.visible = true;                         // Show the mouse cursor
            Cursor.lockState = CursorLockMode.None;        // Unlock the cursor so it can move freely
        }
        else
        {
            Cursor.visible = false;                        // Hide the mouse cursor
            Cursor.lockState = CursorLockMode.Locked;      // Lock the cursor to the center of the screen
        }
    }
}
