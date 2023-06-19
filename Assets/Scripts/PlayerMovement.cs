using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;
    private Collider2D colli;

    [SerializeField] private LayerMask jumpableGround;

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 22f;
    [SerializeField] private float maxFallingVelocity = 40f;

    private enum MovementState { idle, running, jumping, falling }

    [SerializeField] private AudioSource jumpSound;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        colli = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        UpdateAnimationState(dirX);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSound.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (rb.velocity.y < -maxFallingVelocity)
        {
            rb.velocity = new Vector2(rb.velocity.x, -maxFallingVelocity);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(colli.bounds.center, colli.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void UpdateAnimationState(float dirX)
    {
        MovementState movementState;

        if (dirX < 0)
        {
            sr.flipX = true;
            movementState = MovementState.running;
        }
        else if (dirX > 0)
        {
            sr.flipX = false;
            movementState = MovementState.running;
        }
        else
        {
            movementState = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            movementState = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            movementState = MovementState.falling;
        }

        animator.SetInteger("movementState", (int)movementState);
    }
}
