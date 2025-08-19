using UnityEngine;

public class TableCase1 : MonoBehaviour, IInteractable
{
    private bool IsTableCaseOpen = false;

    [SerializeField] AudioSource SFXSource;

    public AudioClip TableCaseOpenSound;
    public AudioClip TableCaseCloseSound;

    public void Interact()
    {
        Animator ani = GetComponent<Animator>();

        if (!IsTableCaseOpen)
        {
            ani.Play("TableCaseOpen 1");
            IsTableCaseOpen = true;
        }

        else
        {
            ani.Play("TableCaseClose 1");
            IsTableCaseOpen = false;
        }
    }

    public void openDoorPlay()
    {
        SFXSource.PlayOneShot(TableCaseOpenSound);
    }

    public void closeDoorPlay()
    {
        SFXSource.PlayOneShot(TableCaseCloseSound);
    }
}
