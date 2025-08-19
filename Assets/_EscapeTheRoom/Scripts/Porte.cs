using UnityEngine;

public class Porte : MonoBehaviour, IInteractable
{
    private bool IsOpen = false;

    [SerializeField] AudioSource SFXSource;

    public AudioClip PorteCaseOpenSound;
    public AudioClip PorteCaseCloseSound;

    public void Interact()
    {
        Animator ani = GetComponent<Animator>();

        if (!IsOpen)
        {
            ani.Play("PorteOpen");
            IsOpen = true;
        }

        else
        {
            ani.Play("PorteClose");
            IsOpen = false;
        }
    }

    public void openDoorPlay()
    {
        SFXSource.PlayOneShot(PorteCaseOpenSound);
    }

    public void closeDoorPlay()
    {
        SFXSource.PlayOneShot(PorteCaseCloseSound);
    }
}
