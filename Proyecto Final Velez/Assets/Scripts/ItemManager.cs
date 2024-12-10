using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private ItemControl[] items;
    [SerializeField] private PlayerControl playerControl;

    private void Update()
    {
        OnItemDestroyed();
    }
    public void OnItemDestroyed()
    {
        bool allDestroyed = true;
        for (int i = 0; i < items.Length; ++i)
        {
            if (items[i] != null) 
            {
                allDestroyed = false; 
                break; 
            }
        }
        if (allDestroyed)
        {
            playerControl.UnlockCursor();
            SceneManager.LoadScene("Victory");
        }
    }
}
