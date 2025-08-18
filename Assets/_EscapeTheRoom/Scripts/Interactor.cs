using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Required for using TextMeshPro UI elements

// Interface that defines an interactable object
interface IInteractable
{
    public void Interact(); // Method to be called when the object is interacted with
}

// Component that handles player interaction with interactable objects
public class Interactor : MonoBehaviour
{
    // The origin point for the raycast (usually the player's camera)
    public Transform InteractorSource;

    // Maximum distance the player can interact with an object
    public float InteractRange;

    // Reference to the UI GameObject that shows "Press E to interact"
    public GameObject interactUI;

    // Keeps track of the currently detected interactable object
    private IInteractable currentInteractable;

    void Update()
    {
        // Create a ray starting at the InteractorSource and pointing forward
        Ray r = new Ray(InteractorSource.position, InteractorSource.forward);

        // Cast the ray and check if it hits something within InteractRange
        if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
        {
            // Try to get an IInteractable component from the hit object
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                // Store reference to the detected interactable object
                currentInteractable = interactObj;

                // Show the interaction UI if it's not already visible
                if (interactUI != null && !interactUI.activeSelf)
                {
                    interactUI.SetActive(true);
                }

                // Check if the player presses the 'E' key to interact
                if (Input.GetKeyDown(KeyCode.E))
                {
                    currentInteractable.Interact(); // Call the Interact() method
                }

                return; // Exit here so we don't hide the UI right after showing it
            }
        }

        // If no interactable object is detected in front of the player:
        // - hide the UI
        // - clear the currentInteractable reference
        if (interactUI != null && interactUI.activeSelf)
        {
            interactUI.SetActive(false);
        }

        currentInteractable = null;
    }
}
