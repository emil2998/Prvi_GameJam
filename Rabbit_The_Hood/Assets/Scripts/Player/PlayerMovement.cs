
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 1f;
    [SerializeField] private float gravity = -9.81f;

    private Vector3 moveInput;
    private Vector3 velocity;

    private Rigidbody playerRigidbody;
    private CharacterController characterController;

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private bool shouldFaceMoveDirection = false;

    [SerializeField] private Transform yawTarget;
    private Vector3 forward;
    private Vector3 right;

    public bool isAiming;

    private void Awake()
    {
        playerRigidbody =  GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
    }

    public void MovePlayer(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log(moveInput);
    }

    public void PlayerJump(InputAction.CallbackContext context)
    {
        if (context.performed  && characterController.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -1f * gravity);

        }
    }

    private void Update()
    {
      Vector3 moveDirection = Vector3.zero;
        if (isAiming)
        {
            forward = transform.forward;
            right = transform.right;

            forward.y = 0;
            right.y = 0;

            forward.Normalize();
            right.Normalize();

            moveDirection = forward * moveInput.y + right * moveInput.x;
        }
        else
        {
            forward = cameraTransform.forward;
            right = cameraTransform.right;

            forward.y = 0;
            right.y = 0;

            forward.Normalize();
            right.Normalize();

            moveDirection = forward * moveInput.y + right * moveInput.x;
        }
           
        characterController.Move(moveDirection * speed * Time.deltaTime);

        if (isAiming)
        {
            Vector3 lookDirection = yawTarget.forward;
            lookDirection.y = 0;

            if(lookDirection.sqrMagnitude > 0.01f)
            {
                Quaternion rotation = Quaternion.LookRotation(lookDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 20f * Time.deltaTime);
            }
        }
        else if (shouldFaceMoveDirection && moveDirection.sqrMagnitude > 0.001f)
        {
            Quaternion rotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 20f * Time.deltaTime);
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

    }





    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out TerrainCollider terrainCollider))
        {
          //  isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out TerrainCollider terrainCollider))
        {
           // isGrounded = false;
        }
    }
}
