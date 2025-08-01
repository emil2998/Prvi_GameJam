using UnityEngine;

public class PlayerPickup : MonoBehaviour
{

    [SerializeField] private UIManager manager;
    public bool keyPickedUp = false;
    public bool lockedChestCollected = false;
    public bool unlockedChestCollected = false;

    private void Start()
    {
        keyPickedUp = false;
        lockedChestCollected = false;
        unlockedChestCollected = false;
    }

    private void Update()
    {
        if (lockedChestCollected)
        {
            manager.UpdateText();
        }

        if (unlockedChestCollected)
        {
            manager.UpdateText();
        }
    }
}
