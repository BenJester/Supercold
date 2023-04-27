using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Ground : MonoBehaviour
{
    /// <summary>
    /// BoxCollider2D
    /// </summary>
    private BoxCollider2D boxCollider2D;

    void Awake() {
        // Cache
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    /// <summary>
    /// Helper to get the minimum y position on the bounding box.
    /// </summary>
    /// <returns></returns>
    public float GetBoundsMinYPosition() {
        return boxCollider2D.bounds.min.y;
    }
}
