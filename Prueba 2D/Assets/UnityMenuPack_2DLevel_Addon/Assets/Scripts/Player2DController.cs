using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Player2DController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float jumpForce = 12f;
    public LayerMask groundMask;
    public Transform groundCheck;
    public float groundRadius = 0.15f;

    Rigidbody2D rb;
    bool controlEnabled = true;

    void Awake() { rb = GetComponent<Rigidbody2D>(); }

    void Update()
    {
        if (!controlEnabled) return;
        float x = Input.GetAxisRaw("Horizontal");
        Vector2 v = rb.linearVelocity;
        v.x = x * moveSpeed;
        rb.linearVelocity = v;
        if (x != 0)
        {
            var scale = transform.localScale;
            scale.x = Mathf.Sign(x) * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public bool IsGrounded()
    {
        if (groundCheck == null) return false;
        return Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundMask) != null;
    }

    public void DisableControl()
    {
        controlEnabled = false;
        rb.linearVelocity = Vector2.zero;
        rb.isKinematic = true;
    }
}
