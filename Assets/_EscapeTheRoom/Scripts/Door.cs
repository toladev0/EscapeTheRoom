using UnityEngine;

// This class represents a door that can be interacted with
public class Door : MonoBehaviour, IInteractable
{
    // Tracks whether the door is currently open or closed
    private bool IsDoorOpen = false;
    public bool isNagative;

    // Reference to the AudioSource component used to play sound effects
    [SerializeField] AudioSource SFXSource;

    // Audio clips for opening and closing the door
    public AudioClip openDoor;
    public AudioClip closeDoor;

    // Called when the player interacts with the door
    public void Interact()
    {
        // Get the Animator component attached to this GameObject
        Animator ani = GetComponent<Animator>();

        // If the door is closed, play the opening animation and mark it as open
        if (!IsDoorOpen)
        {
            if(!isNagative)
                ani.Play("DoorOpen");
            else
                ani.Play("DoorOpenNgative");
            IsDoorOpen = true;
        }
        // If the door is open, play the closing animation and mark it as closed
        else
        {
            if(!isNagative)
                ani.Play("DoorClose");
            else
                ani.Play("DoorCloseNgative");
            IsDoorOpen = false;
        }
    }

    // Play the open door sound effect
    public void openDoorPlay()
    {
        SFXSource.PlayOneShot(openDoor);
    }

    // Play the close door sound effect
    public void closeDoorPlay()
    {
        SFXSource.PlayOneShot(closeDoor);
    }
}
