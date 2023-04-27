using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class Trigger2D : MonoBehaviour
{
    public abstract void Activate(Collider2D other);
    public abstract void Deactivate(Collider2D other);

    protected BoxCollider2D boxCollider2D;

    protected void Awake() {
        // Cache and setup
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.isTrigger = true;
    }

    /// <summary>
    /// Triggered when another Collider2D enters the bounding box.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other) {
        Player2D player = other.GetComponent<Player2D>();
        if (player == null) return;
        Activate(other);
    }

    /// <summary>
    /// Triggered when another Collider2D leaves the bounding box.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other) {
        Player2D player = other.GetComponent<Player2D>();
        if (player == null) return;
        Deactivate(other);
    }

    /// <summary>
    /// Helper method to get the Box Collider 2D.
    /// </summary>
    /// <returns></returns>
    public BoxCollider2D GetCollider() {
        return boxCollider2D;
    }

    /// <summary>
    /// Draw gizmos.
    /// </summary>
    protected virtual void OnDrawGizmos() {
        if (boxCollider2D == null) return;

        Gizmos.DrawWireCube(boxCollider2D.transform.position, boxCollider2D.size);
    }
}
