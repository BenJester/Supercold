using UnityEngine;

[System.Serializable]
public class Target2D {

    /// <summary>
    /// The target game object.
    /// </summary>
    public GameObject target;
    
    /// <summary>
    /// The last position of the target. Helps calculate the velocity.
    /// </summary>
    private Vector3 lastPos;

    /// <summary>
    /// Is this target the player.
    /// Used to help identify the target.
    /// </summary>
    public bool isPlayer = false;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="isPlayer"></param>
    public Target2D(GameObject target, bool isPlayer) {
        this.target = target;
        this.isPlayer = isPlayer;
    }

    /// <summary>
    /// Gets the target's velocity.
    /// </summary>
    /// <returns></returns>
    public Vector2 GetVelocity() {
        Vector3 velocity = (target.transform.position - lastPos) / Time.deltaTime;
        lastPos = target.transform.position;
        return velocity;
    }
}
