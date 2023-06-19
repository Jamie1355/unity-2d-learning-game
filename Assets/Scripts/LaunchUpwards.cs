using UnityEngine;

public class LaunchUpwards : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private float launchVelocity = 100f;
    [SerializeField] private AudioSource launchSound;

    private bool hasLaunched = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !hasLaunched)
        {
            hasLaunched = true;
            animator.SetTrigger("launch");
            launchSound.Play();

            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(rb.velocity.x, launchVelocity);
        }
    }

    public void ResetLaunch()
    {
        hasLaunched = false;
    }
}
