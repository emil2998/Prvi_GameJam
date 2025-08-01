using UnityEngine;

public class DoorContact : MonoBehaviour
{
    [SerializeField] private Transform leftDoor;
    [SerializeField] private Transform rightDoor;
    public bool IsPlayerContactingDoor { get; private set; }

    private bool isOpenLeft = false;
    private bool isOpenRight = false;

    private void Start()
    {
        isOpenLeft = false;
        isOpenRight = false;    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerHP player))
        {
            IsPlayerContactingDoor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerHP player))
        {
            IsPlayerContactingDoor = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && IsPlayerContactingDoor)
        {
            if (!isOpenLeft)
            {
                isOpenLeft = true;
                leftDoor.Rotate(0, 0, -80);
                
                
            }
            else if (isOpenLeft)
            {
                isOpenLeft = false;
                leftDoor.Rotate(0, 0, 80);

            }

            if (!isOpenRight)
            {
                isOpenRight = true;
                rightDoor.Rotate(0, 0, 80);             
                
            }
            else if (isOpenRight)
            {
                isOpenRight = false;
                rightDoor.Rotate(0, 0, -80);

            }

        }
    }
}
