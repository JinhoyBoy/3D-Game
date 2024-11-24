using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    private Animator animator;
    [SerializeField] AudioSource jumpAudio;
    [SerializeField] AudioSource bounceAudio;

    [SerializeField] private float speed = 6;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3;
    private float initialSpeed;
    private float initialJumpHeight;

    Vector3 velocity;
    bool isGrounded;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private bool isBouncing = false;
    private float bounceTime = 0.2f; // Duration of the bounce effect
    private float bounceTimer;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        initialSpeed = speed;
        initialJumpHeight = jumpHeight;
    }

    void Update()
    {
        // Jump and Ground Check
        bool wasGrounded = isGrounded;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        animator.SetBool("isGrounded", isGrounded);

        // Reset bounce velocity after landing
        if (isGrounded && !wasGrounded && velocity.y < 0)
        {
            velocity = Vector3.zero;
            isBouncing = false;
        }

        // Apply gravity while not bouncing
        if (!isBouncing)
        {
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
                jumpAudio.Play();
            }

            velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            // Decrease bounce effect gradually during bounce
            bounceTimer -= Time.deltaTime;
            if (bounceTimer <= 0)
            {
                isBouncing = false;
            }
        }

        controller.Move(velocity * Time.deltaTime);

        // Walk
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            animator.SetBool("isMoving", true);

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        // Respawn when falling off the map
        if (transform.position.y < -50)
        {
            transform.position = new Vector3(0, 3, 0);
        }

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        switch (hit.gameObject.tag)
        {
            case "SpeedPad":
                speed = 16;
                break;
            case "JumpPad":
                jumpHeight = 5;
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
                jumpAudio.Play();
                break;
            case "BouncePad":
                // Apply initial bounce impulse
                Vector3 bounceDirection = Vector3.Reflect(transform.forward, hit.normal);
                bounceDirection.Normalize();
                velocity = bounceDirection * 5;
                velocity.y = Mathf.Sqrt(0.5f * -2 * gravity);

                // Set up the bounce timer
                isBouncing = true;
                bounceTimer = bounceTime;

                bounceAudio.Play();
                break;
            default:
                speed = initialSpeed;
                jumpHeight = initialJumpHeight;
                break;
        }
    }
}
