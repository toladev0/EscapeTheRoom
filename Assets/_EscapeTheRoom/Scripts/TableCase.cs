using UnityEngine;

public class TableCase : MonoBehaviour, IInteractable
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
            ani.Play("TableCaseOpen");
            IsTableCaseOpen = true;
        }

        else
        {
            ani.Play("TableCaseClose");
            IsTableCaseOpen = false;
        }
    }

    public void openPlay()
    {
        SFXSource.PlayOneShot(TableCaseOpenSound);
    }

    public void closePlay()
    {
        SFXSource.PlayOneShot(TableCaseCloseSound);
    }
}
