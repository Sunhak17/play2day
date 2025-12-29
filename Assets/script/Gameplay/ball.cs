using UnityEngine;

public class ball : MonoBehaviour
{
    [Header("Ball Physics")]
    public float bounciness = 0.7f; // How bouncy the ball is (0-1)
    public float mass = 0.4f; // Lighter ball for realistic soccer physics
    public float drag = 0.5f; // Air resistance
    public float angularDrag = 0.5f; // Rotation resistance

    private Rigidbody2D rb;
    private PhysicsMaterial2D bouncyMaterial;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody2D found on this GameObject. Please add a Rigidbody2D component.");
            return;
        }

        // Create and apply bouncy physics material
        bouncyMaterial = new PhysicsMaterial2D("BallMaterial");
        bouncyMaterial.bounciness = bounciness;
        bouncyMaterial.friction = 0.4f; // Some friction for realistic rolling

        // Apply material to collider
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.sharedMaterial = bouncyMaterial;
        }

        // Configure Rigidbody2D settings
        rb.mass = mass;
        rb.drag = drag;
        rb.angularDrag = angularDrag;
        rb.gravityScale = 1f; // Normal gravity
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; // Better collision detection
    }
}
