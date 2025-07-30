using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 1f;
    [SerializeField] private float gravity = -9.81f;

    private Vector3 moveInput;
    private Vector3 velocity;

    private CharacterController characterController;

    [SerializeField] private Vector3 moveDirection;

    private Vector3 forward;
    private Vector3 right;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void WalkForward(bool isWalking)
    {
        animator.SetBool("WalkForward", isWalking);
        
    }
    public void WalkBack(bool isWalking)
    {
        animator.SetBool("WalkBack", isWalking);

    }
    public void WalkRight(bool isWalking)
    {
        animator.SetBool("WalkRight", isWalking);

    }
    public void WalkLeft(bool isWalking)
    {
        animator.SetBool("WalkLeft", isWalking);

    }


    public void MovePlayer(InputAction.CallbackContext context)
    {
        
        moveInput = context.ReadValue<Vector2>();

       Debug.Log(moveInput);
        if(moveInput == Vector3.up)
        {
            WalkForward(true);
        }
        else
        {
            WalkForward(false);
        }
        
        if (moveInput == -Vector3.up)
        {
            WalkBack(true);
        }
        else
        {
            WalkBack(false);
        }

        if (moveInput == Vector3.right)
        {
            WalkRight(true);
        }
        else
        {
            WalkRight(false);
        }

        if (moveInput == -Vector3.right)
        {
            WalkLeft(true);
        }
        else
        {
            WalkLeft(false);
        }
    }

    public void PlayerJump(InputAction.CallbackContext context)
    {
        if (context.performed && characterController.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -1f * gravity);
        }
    }

    private void Update()
    {
        forward = transform.forward;
        right = transform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();
        moveDirection = forward * moveInput.y + right * moveInput.x;
        characterController.Move(moveDirection * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        
    }
}
