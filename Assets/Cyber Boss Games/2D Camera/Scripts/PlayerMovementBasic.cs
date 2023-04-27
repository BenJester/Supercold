using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D), typeof(SpriteRenderer))]
public class PlayerMovementBasic : MonoBehaviour
{
    /// <summary>
    /// The movement speed of the player.
    /// </summary>
    public float movementSpeed = 0.05f;

    /// <summary>
    /// The force to apply to make the player jump.
    /// </summary>
    public float jumpForce = 5;

    /// <summary>
    /// If the player is on the ground.
    /// </summary>
    private bool isGrounded = false;

    /// <summary>
    /// Rigidbody2D
    /// </summary>
    private Rigidbody2D rb;

    /// <summary>
    /// SpriteRenderer
    /// </summary>
    private SpriteRenderer spriteRenderer;

    void Awake() {
        // Cache
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Main loop
    /// </summary>
    void FixedUpdate() {
        float h = Input.GetAxisRaw("Horizontal");

        // Move Right
        if (h > 0) {
            transform.position += Vector3.right * movementSpeed;
            spriteRenderer.flipX = false;
        }

        // Move Left
        if (h < 0) {
            transform.position += Vector3.left * movementSpeed;
            spriteRenderer.flipX = true;
        }
    }

    /// <summary>
    /// Main loop.
    /// </summary>
    void Update() {
        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    /// <summary>
    /// Triggered when another Collision2D enters the bounding box.
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.GetComponent<Ground>() != null) {
            isGrounded = true;
        }
    }

    /// <summary>
    /// Triggered when another Collision2D exits the bounding box.
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionExit2D(Collision2D other) {
         if (other.gameObject.GetComponent<Ground>() != null) {
            isGrounded = false;
        }   
    }
}
