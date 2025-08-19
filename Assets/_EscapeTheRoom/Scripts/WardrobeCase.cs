using UnityEngine;

public class WardrobeCase : MonoBehaviour, IInteractable
{
    private bool IsOpen = false;

    [SerializeField] AudioSource SFXSource;

    public AudioClip OpenSound;
    public AudioClip CloseSound;

    public void Interact()
    {
        Animator ani = GetComponent<Animator>();

        if (!IsOpen)
        {
            ani.Play("WardrobeCaseOpen");
            IsOpen = true;
        }

        else
        {
            ani.Play("WardrobeCaseClose");
            IsOpen = false;
        }
    }

    public void openPlay()
    {
        SFXSource.PlayOneShot(OpenSound);
    }

    public void closePlay()
    {
        SFXSource.PlayOneShot(CloseSound);
    }
}
