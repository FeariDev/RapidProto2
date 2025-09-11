using UnityEngine;

public class PlayerDirectionController : MonoBehaviour
{
   
    public float moveSpeed = 15f;
    private Rigidbody2D rb;
    private bool facingRight = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float input = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(input * moveSpeed, rb.linearVelocity.y);

        // Flipataan kun suunta vaihtuu
        if (input > 0 && !facingRight) Flip();
        if (input < 0 && facingRight) Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}


