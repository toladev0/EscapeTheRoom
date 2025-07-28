using UnityEngine;

public class CursorToggle : MonoBehaviour
{
    void Update()
    {
        if (!this.gameObject.activeSelf)
        {
            Cursor.visible = true;         // show the cursor
            Cursor.lockState = CursorLockMode.None; // unlock cursor
        }
        else
        {
            Cursor.visible = false;        // hide the cursor
            Cursor.lockState = CursorLockMode.Locked; // lock cursor to center
        }
    }
}
