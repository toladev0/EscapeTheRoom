using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwich : MonoBehaviour, IInteractable
{
    public GameObject lightObject;
    private bool isON = false;

    public void Interact()
    {
        if (!isON)
        {
            lightObject.SetActive(true);
            isON = true;
        }
        else
        {
            lightObject.SetActive(false);
            isON = false;
        }
    }
}
