using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainKey : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Destroy(gameObject);
    }
}
