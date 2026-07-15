using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;
    public Transform head;
    public Camera camera;

    [Header("Configurations")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float mouseSensitivity = 2f;
    public float jumpForce = 4.5f;

    [HideInInspector] public Vector3 spawnPosition;

    public bool canMove = true;

    private float currentSpeed;
    private float xRotation = 0f;

    private float speedMultiplier = 1f;

    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;

        // Lock Cursor to the Center of the Screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        MovePlayer();
        HandleMouseLook();
        HandleJump();
    }

    void MovePlayer()
    {
        if (!canMove)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            return;
        }

        // Sprint
        float baseSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        // Yellow Light Speed
        currentSpeed = baseSpeed * speedMultiplier;

        // Get Input Axes
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Determine Movement Direction Relative to Camera
        Vector3 direction = camera.transform.forward * vertical + camera.transform.right * horizontal;
        direction.Normalize();
        direction.y = 0f;

        // Apply velocity
        Vector3 moveVelocity = direction * currentSpeed;
        rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);
    }

    void HandleJump()
    {
        if (!canMove) return;

        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate the player horizontally
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera vertically
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        head.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    public bool IsMoving()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        return horizontal != 0 || vertical != 0 || rb.velocity.magnitude > 0.1f;
    }

    public void FreezePlayer()
    {
        canMove = false;
        rb.velocity = Vector3.zero;
    }

    public void UnfreezePlayer()
    {
        canMove = true;
    }

    // Yellow Light Speed
    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }

    public void ResetToSpawn()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.isKinematic = true;
        transform.position = spawnPosition;
        rb.isKinematic = false;
    }
}
