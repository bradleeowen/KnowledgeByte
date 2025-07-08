using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public float chasespeed = 2f;
    public float jumpforce = 5f;

    private Transform player;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool shouldJump;

    public int damage = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        // ✅ Use proper ground detection with OverlapCircle
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        float direction = Mathf.Sign(player.position.x - transform.position.x);
        bool isPlayerAbove = player.position.y > transform.position.y;

        // ✅ Allow movement in air for better chasing
        
        if (isGrounded)
        {
            // ✅ Check for wall in front
            Vector2 wallCheckOrigin = transform.position + new Vector3(direction * 0.5f, 0, 0);
            RaycastHit2D wallInFront = Physics2D.Raycast(wallCheckOrigin, Vector2.right * direction, 0.5f, groundLayer);

            // ✅ Check for platform above
            RaycastHit2D ceiling = Physics2D.Raycast(transform.position, Vector2.up, 2f, groundLayer);

            if (isGrounded)
                            {
    bool wallHit = wallInFront.collider != null;
    bool playerAbove = player.position.y > transform.position.y + 1f;

    if (wallHit || playerAbove)
    {
        shouldJump = true;
        Debug.Log("Jump triggered");
    }
             }

        }
        Debug.Log("Grounded: " + isGrounded);

    }

    private void FixedUpdate()
    {   
        if (isGrounded && shouldJump)
        {
            shouldJump = false;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // Reset vertical motion
            rb.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse); // Clean vertical jump
        }
    }

    // ✅ Optional: Show ground check in Scene view for debugging
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
