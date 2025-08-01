using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text treasuresFoundText;
    [SerializeField] private PlayerPickup playerPickup;
    private void Start()
    {
        treasuresFoundText.text = "Treasures found: 0 / 2";
    }

    public void UpdateText()
    {
        if(playerPickup.unlockedChestCollected || playerPickup.lockedChestCollected)
        {
            treasuresFoundText.text = "Treasures found: 1 / 2";
        }
        if (playerPickup.unlockedChestCollected && playerPickup.lockedChestCollected)
        {
            treasuresFoundText.text = "Treasures found: 2 / 2";
        }
    }
}
