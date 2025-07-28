using UnityEngine;
using UnityEngine.EventSystems;

// This script automatically selects a default UI button when the player uses keyboard/controller input.
// It ensures keyboard/controller navigation works even if the player previously used a mouse.
public class SetFirstSelectedOnKeyboard : MonoBehaviour
{
    [Header("Button to Select")]
    public GameObject firstSelectedButton; // The UI button that should be selected when using keyboard/controller

    private bool hasSelected = false; // Tracks whether the selection has already been set

    // Called when this GameObject becomes active (e.g., when switching menus)
    private void OnEnable()
    {
        hasSelected = false;

        // Clear any previous UI selection so that the current menu can set its own
        EventSystem.current.SetSelectedGameObject(null);
    }

    void Update()
    {
        // Detect if keyboard/controller movement input is active
        bool usingController = Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f;

        // If a key or controller input is used and no selection has been made yet
        if (!hasSelected && (Input.anyKeyDown || usingController))
        {
            // Ignore mouse button clicks
            if (!Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1))
            {
                GameObject current = EventSystem.current.currentSelectedGameObject;

                // Only set selection if none is selected OR the selected object is inactive OR not part of this menu
                if (current == null || !current.activeInHierarchy || !current.transform.IsChildOf(this.transform))
                {
                    if (firstSelectedButton != null)
                    {
                        // Set the specified button as the currently selected object
                        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
                        hasSelected = true; // Prevent re-selecting repeatedly
                    }
                }
            }
        }

        // If the player uses the mouse (click), clear the current selection
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            hasSelected = false; // Allow re-selection if user returns to keyboard/controller
            EventSystem.current.SetSelectedGameObject(null); // Remove current selection
        }
    }
}
