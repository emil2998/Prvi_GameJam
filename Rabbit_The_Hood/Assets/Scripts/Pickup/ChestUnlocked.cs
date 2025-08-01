
using TMPro;
using UnityEngine;

public class ChestUnlocked : MonoBehaviour
{
    private bool isInsideArea = false;
    private PlayerPickup playerPickup;

    [SerializeField] private GameObject treasure;

    [SerializeField] private TMP_Text treasureText;
    private void Start()
    {
        treasureText.text = "";
        isInsideArea = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out PlayerPickup player))
        {
            playerPickup = player;
            isInsideArea = true;
            treasureText.text = "Press E to collect the treasure!";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerPickup player))
        {
            isInsideArea = false;
            treasureText.text = "";         
        }
    }

    private void Update()
    {
        if (isInsideArea && Input.GetKeyDown(KeyCode.E) && gameObject.name == "ChestUnlocked")
        {
            playerPickup.unlockedChestCollected = true;
            treasureText.text = "";
            treasure.SetActive(false);
        }
    }
}
