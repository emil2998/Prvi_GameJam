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

    private float currentHorizontal = 0f;
    private float currentVertical = 0f;

    private float horizontalVelocity = 0f;
    private float verticalVelocity = 0f;

    private PlayerControls inputActions;


    [SerializeField] private float animationSmoothTime = 0.15f;

    private void Awake()
    {
        inputActions = new PlayerControls();
        characterController = GetComponent<CharacterController>();
    }


    public void MovePlayer(InputAction.CallbackContext context)
    {
        
        moveInput = context.ReadValue<Vector2>();
    }
    
    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.CameraControls.Aim.performed += Shoot;   
        inputActions.CameraControls.Aim.canceled += Shoot;    
    }

    private void OnDisable()
    {
        inputActions.CameraControls.Aim.performed -= Shoot;
        inputActions.CameraControls.Aim.canceled -= Shoot;
        inputActions.Disable();
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Right Mouse Button clicked!");
            animator.SetBool("Aiming", true);
        }
        else
        {
            animator.SetBool("Aiming", false);
        }
    }

    public void PlayerJump(InputAction.CallbackContext context)
    {
        if (context.performed && characterController.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -1f * gravity);

            if(moveInput != Vector3.zero)
            {
                animator.SetBool("RunJump", true);
            }
            else
            {
                animator.SetBool("Jump", true);
            }          
        }
        else
        {
  
            animator.SetBool("RunJump", false);
            animator.SetBool("Jump", false);
            
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


        currentHorizontal = Mathf.SmoothDamp(currentHorizontal, moveInput.x, ref horizontalVelocity, animationSmoothTime);
        currentVertical = Mathf.SmoothDamp(currentVertical, moveInput.y, ref verticalVelocity, animationSmoothTime);

        animator.SetFloat("Horizontal", currentHorizontal);
        animator.SetFloat("Vertical", currentVertical);

    }
}
