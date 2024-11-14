using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControl : MonoBehaviour
{
    [SerializeField] private string itemName;

    public void Interact()
    {
        Destroy(gameObject);
    }
}
