using UnityEngine;

/// <summary>
/// Handles player movement, gravity direction changes,
/// jumping with coyote/jump-buffer support,
/// rotation alignment based on camera direction,
/// and ground detection using an overlap sphere.
/// </summary>
public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float rotationSmooth = 10f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float jumpBufferTime = 0.15f;
    [SerializeField] private float coyoteTime = 0.1f;

    [Header("Gravity")]
    [SerializeField] private float gravityStrength = 20f;
    private Vector3 gravityDirection = Vector3.down;

    [Header("References")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform visualRoot;
    [SerializeField] private Animator animator;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckRadius = 0.25f;
    [SerializeField] private LayerMask groundMask = ~0;
    private Rigidbody rb;
    private Vector2 moveInput;

    private float jumpBufferCounter;
    private float coyoteCounter;

    public bool IsGrounded => CheckGrounded();
    public Vector3 CurrentGravityDirection() => gravityDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.freezeRotation = true;
    }

    private void Update()
    {
        ReadInput();
        HandleTimers();
        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        ApplyGravity();
        HandleJump();
    }

    private void ReadInput()
    {
        moveInput = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) moveInput.y = 1f;
        if (Input.GetKey(KeyCode.S)) moveInput.y = -1f;
        if (Input.GetKey(KeyCode.A)) moveInput.x = -1f;
        if (Input.GetKey(KeyCode.D)) moveInput.x = 1f;
    }

    private void HandleTimers()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            jumpBufferCounter = jumpBufferTime;
        else
            jumpBufferCounter -= Time.deltaTime;

        if (IsGrounded)
            coyoteCounter = coyoteTime;
        else
            coyoteCounter -= Time.deltaTime;

        jumpBufferCounter = Mathf.Max(0f, jumpBufferCounter);
        coyoteCounter = Mathf.Max(0f, coyoteCounter);
    }

    private void HandleMovement()
    {
        Vector3 camForward = Vector3.ProjectOnPlane(cameraTransform.forward, -gravityDirection).normalized;
        Vector3 camRight = Vector3.ProjectOnPlane(cameraTransform.right, -gravityDirection).normalized;

        Vector3 moveDir = camForward * moveInput.y + camRight * moveInput.x;
        if (moveDir.sqrMagnitude > 1f) moveDir.Normalize();

        Vector3 targetPosition = rb.position + moveDir * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(targetPosition);

        if (visualRoot && moveDir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveDir, -gravityDirection);
            visualRoot.rotation = Quaternion.Slerp(
                visualRoot.rotation,
                targetRot,
                rotationSmooth * Time.deltaTime
            );
        }
    }

    private void ApplyGravity()
    {
        rb.AddForce(gravityDirection * gravityStrength, ForceMode.Acceleration);
    }

    private void HandleJump()
    {
        if (jumpBufferCounter > 0f && coyoteCounter > 0f && IsGrounded)
        {
            rb.AddForce(-gravityDirection * jumpForce, ForceMode.VelocityChange);
            animator.SetTrigger("Jump");

            jumpBufferCounter = 0f;
            coyoteCounter = 0f;
        }
    }



    private bool CheckGrounded()
    {
        if (!groundCheckPoint) return false;


        Collider[] hits = Physics.OverlapSphere(
            groundCheckPoint.position,
            groundCheckRadius,
            groundMask,
            QueryTriggerInteraction.Ignore
        );

        foreach (var hit in hits)
        {
            if (hit.gameObject != gameObject)
                return true;
        }

        return false;
    }


    private void OnDrawGizmosSelected()
    {
        if (!groundCheckPoint) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
    }



    private void UpdateAnimator()
    {
        float speed = moveInput.magnitude;
        animator.SetFloat("Speed", speed);
        Debug.LogWarning(speed);
        animator.SetBool("Grounded", IsGrounded);
    }

    public void SetGravityDirection(Vector3 newDir)
    {
        gravityDirection = newDir.normalized;

        if (visualRoot)
        {
            Quaternion targetRot =
                Quaternion.FromToRotation(visualRoot.up, -gravityDirection) * visualRoot.rotation;

            visualRoot.rotation = targetRot;
        }
    }
}