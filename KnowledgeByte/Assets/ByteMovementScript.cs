using UnityEngine;

public class ByteMovementScript : MonoBehaviour
{
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    
    void Start()
    {
      rb = GetComponent<Rigidbody2D>();  
    }

    
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        } 
    }
}
