using UnityEngine;
using UnityEngine.EventSystems;

public class SetFirstSelectedOnKeyboard : MonoBehaviour
{
    [Header("Button to Select")]
    public GameObject firstSelectedButton;

    private bool hasSelected = false;

    private void OnEnable()
    {
        hasSelected = false;
        // Clear selection so new menu can take control
        EventSystem.current.SetSelectedGameObject(null);
    }

    void Update()
    {
        bool usingController = Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f;

        // Force selection if keyboard/controller is used AND current selected is not part of this menu
        if (!hasSelected && (Input.anyKeyDown || usingController))
        {
            if (!Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1))
            {
                GameObject current = EventSystem.current.currentSelectedGameObject;

                // Only set if nothing is selected OR selected object is inactive (e.g., from previous menu)
                if (current == null || !current.activeInHierarchy || !current.transform.IsChildOf(this.transform))
                {
                    if (firstSelectedButton != null)
                    {
                        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
                        hasSelected = true;
                    }
                }
            }
        }

        // Mouse interaction clears selection
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            hasSelected = false;
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
