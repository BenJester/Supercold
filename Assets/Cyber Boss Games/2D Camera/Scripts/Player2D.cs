using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player2D : MonoBehaviour
{
    /// <summary>
    /// Rigibody2D
    /// </summary>
    private new Rigidbody2D rigidbody2D;

    /// <summary>
    /// Used to help calculate the velocity.
    /// </summary>
    private Vector3 lastPos;

    void Awake() {
        // Cache and setup
        rigidbody2D = GetComponent<Rigidbody2D>();
        lastPos = transform.position;
    }

    /// <summary>
    /// Helper to get the players velocity.
    /// </summary>
    /// <returns></returns>
    public Vector2 GetVelocity() {
        Vector3 velocity = (transform.position - lastPos) / Time.deltaTime;
        lastPos = transform.position;
        return velocity;
    }
}
