using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float chasespeed = 2f;
    public float jumpforce = 2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool shouldJump;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);
        float direction = Mathf.Sign(player.position.x - transform.position.x);
        bool isPlayerAbove = player.position.y > transform.position.y;

        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(direction * chasespeed, rb.linearVelocity.y);

            RaycastHit2D groundInFront = Physics2D.Raycast(transform.position, new Vector2(direction, 0), 2f, groundLayer);
            RaycastHit2D gapAhead = Physics2D.Raycast(transform.position + new Vector3(direction, 0, 0), Vector2.down, 2f, groundLayer);
            RaycastHit2D platformAbove = Physics2D.Raycast(transform.position, Vector2.up, 3f, groundLayer);

            if (!groundInFront.collider && !gapAhead.collider)
                shouldJump = true;
            else if (isPlayerAbove && platformAbove.collider)
                shouldJump = true;
        }
    }

    void FixedUpdate()
    {
        if (isGrounded && shouldJump)
        {
            shouldJump = false;
            Vector2 jumpDir = new Vector2(Mathf.Sign(player.position.x - transform.position.x), 1).normalized;
            rb.AddForce(jumpDir * jumpforce, ForceMode2D.Impulse);
        }
    }
}
