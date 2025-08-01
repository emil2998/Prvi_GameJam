using TMPro;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public bool playerWonGame = false;
    [SerializeField] private TMP_Text winText;

    [SerializeField] private UIManager uiManager;
    private void Start()
    {
        playerWonGame = false;
        winText.text = "";
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out PlayerPickup player))
        {
            if(player.lockedChestCollected && player.unlockedChestCollected)
            {
                playerWonGame = true;
                winText.text = "You have won the game!";
                uiManager.GameWon();
                Debug.Log("Win");
            }
        }
    }
}
