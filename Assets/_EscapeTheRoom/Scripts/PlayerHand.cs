using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public GameObject[] item;         // Inventory or logical item list
    public GameObject[] itemOnHand;   // Visual representations in hand

    private void Update()
    {
        for (int i = 0; i < item.Length; i++)
        {
            if (item[i] != null)
            {
                itemOnHand[i].SetActive(false);
            }
            else
            {
                itemOnHand[i].SetActive(true);
            }
        }
    }
}
