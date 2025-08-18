using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainDoor : MonoBehaviour, IInteractable
{
    public GameObject winPanel;
    public GameObject mainKey;
    public GameObject message; // Assign in Inspector
    private TextMeshProUGUI messageText;

    private void Start()
    {
        if (message != null)
        {
            messageText = message.GetComponent<TextMeshProUGUI>();
            if (messageText == null)
            {
                Debug.LogError("No TextMeshProUGUI component found on message GameObject.");
            }
        }
        else
        {
            Debug.LogError("Message GameObject not assigned in the inspector.");
        }

        // Make sure message is hidden initially
        message.SetActive(false);
    }

    public void Interact()
    {
        if (mainKey != null && messageText != null)
        {
            messageText.text = "Need key to unlock the door.";
            message.SetActive(true);
            StartCoroutine(HideMessageAfterDelay(3f)); // 5-second delay
        }
        else
        {
            winPanel.SetActive(true);
        }
    }

    private IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        message.SetActive(false);
    }
}
