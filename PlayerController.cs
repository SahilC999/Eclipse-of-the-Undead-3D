using UnityEngine;

/// <summary>
/// Handles player movement, camera control, sprinting, jumping, crouching, and animations.
/// Attach to Player GameObject.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 8f;
    public float crouchSpeed = 2.5f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    [Header("Camera Settings")]
    public Camera playerCamera;
    public float mouseSensitivity = 2f;
    public float cameraPitchLimit = 90f;

    [Header("Player States")]
    public bool isSprinting;
    public bool isCrouching;
    public bool isGrounded;

    [Header("Animations")]
    public Animator playerAnimator;

    private CharacterController controller;
    private Vector3 velocity;
    private float cameraPitch = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
        HandleJumping();
        HandleCrouch();
        ApplyGravity();
        UpdateAnimations();
    }

    // Handles WASD movement
    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        float speed = walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching) { speed = sprintSpeed; isSprinting = true; } 
        else { isSprinting = false; }
        if (isCrouching) { speed = crouchSpeed; }

        controller.Move(move * speed * Time.deltaTime);
    }

    // Handles mouse camera rotation
    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -cameraPitchLimit, cameraPitchLimit);

        playerCamera.transform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }

    // Handles jump logic
    void HandleJumping()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isCrouching)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    // Handles crouch toggle
    void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = !isCrouching;
            controller.height = isCrouching ? 1.0f : 2.0f;
        }
    }

    // Applies gravity to player
    void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    // Updates animations based on state
    void UpdateAnimations()
    {
        if (!playerAnimator) return;

        float moveSpeed = new Vector3(controller.velocity.x, 0, controller.velocity.z).magnitude;
        playerAnimator.SetFloat("Speed", moveSpeed);
        playerAnimator.SetBool("IsSprinting", isSprinting);
        playerAnimator.SetBool("IsCrouching", isCrouching);
    }
}
