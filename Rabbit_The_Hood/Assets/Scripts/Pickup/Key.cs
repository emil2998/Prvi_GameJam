using TMPro;
using UnityEngine;

public class Key : MonoBehaviour
{
    private bool canPickup;
    [SerializeField] private TMP_Text keyText;
    private PlayerPickup playerPickup;

    private void Start()
    {
        keyText.text = "";
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerPickup player)) {

            canPickup = true;
            playerPickup = player;
            keyText.text = "Press E to pickup key!";
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerPickup player))
        {
            canPickup = false;
            keyText.text = "";
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && canPickup)
        {
            playerPickup.keyPickedUp = true;
            keyText.text = "";
            Destroy(gameObject);

        }
    }
}
