using System.Collections;
using TMPro;
using UnityEngine;

public class ChestLocked : MonoBehaviour
{
    private bool isInsideArea = false;
    private bool isUnlocked = false;
    private PlayerPickup playerPickup;

    private bool isCoroutineRunning = false;
    [SerializeField] private GameObject lid;
    [SerializeField] private GameObject treasure;

    [SerializeField] private TMP_Text treasureText;
 
    private void Start()
    {
        treasureText.text = "";
        isCoroutineRunning = false;
        isUnlocked = false;
        isInsideArea = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out PlayerPickup player))
        {
            playerPickup = player;
            isInsideArea = true;
            if (playerPickup.keyPickedUp && !isUnlocked)
            {
                treasureText.text = "Press E to unlock the chest!";
            }
            else if(!playerPickup.keyPickedUp){
                treasureText.text = "You need a key to open this chest!";
            }
            else if (isUnlocked)
            {
                treasureText.text = "Press E to collect treasure.";
            }
            
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
     
        if (isInsideArea && playerPickup.keyPickedUp && Input.GetKeyDown(KeyCode.E) && gameObject.name == "ChestLocked") {
 
            if (lid.transform.rotation.x != 0f)
            {
                lid.transform.Rotate(-90f, 0f, 0f);
                
            }
            
            if (!isCoroutineRunning)
            {
                
                StartCoroutine(Cooldown());
            }
            
        }

     

        if (isInsideArea && playerPickup.keyPickedUp && Input.GetKeyDown(KeyCode.E) && gameObject.name == "ChestLocked" && isUnlocked)
        {
            playerPickup.lockedChestCollected = true;
            treasureText.text = "";
            treasure.SetActive(false);
        }

    }

    private IEnumerator Cooldown()
    {
        isCoroutineRunning= true;
        treasureText.text = "Press E to collect treasure.";
        yield return new WaitForSeconds(0.2f);
        isUnlocked = true;
        isCoroutineRunning=false;   
    }


}
