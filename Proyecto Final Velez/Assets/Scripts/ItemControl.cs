using UnityEngine;

public class ItemControl : MonoBehaviour
{
    [SerializeField] public string itemName;

    public void Interact()
    {
        Destroy(gameObject);
    }
}
