using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Rail2D : Trigger2D
{
    /// <summary>
    /// Holds a list of anchors.
    /// </summary>
    /// <typeparam name="RailAnchor2D"></typeparam>
    /// <returns></returns>
    public List<RailAnchor2D> railAnchors = new List<RailAnchor2D>();

    /// <summary>
    /// Helper to add anchors to the list with a specific name.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public RailAnchor2D AddAnchor(string name) {
        RailAnchor2D anchor = new GameObject("Anchor 1").AddComponent<RailAnchor2D>();
        anchor.transform.SetParent(transform, true);
        anchor.transform.position = GetCollider().bounds.center;
        railAnchors.Add(anchor);
        return anchor;
    }

    /// <summary>
    /// Helper to add anchors to the list with a specific name and position.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="localPosition"></param>
    public void AddAnchor(string name, Vector2 localPosition) {
        RailAnchor2D anchor = new GameObject("Anchor 1").AddComponent<RailAnchor2D>();
        anchor.transform.SetParent(transform, true);
        anchor.transform.position = localPosition;
        railAnchors.Add(anchor);
    }

    /// <summary>
    /// Triggers when another Collider2D enters the bounding box.
    /// </summary>
    /// <param name="other"></param>
    public override void Activate(Collider2D other)
    {
        Camera2D cam = GameObject.FindObjectOfType<Camera2D>();
        if (cam == null) return;

        cam.SetIsOnRail(true, this);
    }

    /// <summary>
    /// Triggers when another Collider2D exits the bounding box.
    /// </summary>
    /// <param name="other"></param>
    public override void Deactivate(Collider2D other)
    {
        Camera2D cam = GameObject.FindObjectOfType<Camera2D>();
        if (cam == null) return;

        cam.SetIsOnRail(false, null);
        cam.ResetOffset();
    }

    /// <summary>
    /// Draw gismos.
    /// </summary>
    protected override void OnDrawGizmos() {
        base.OnDrawGizmos();

        if (boxCollider2D != null) {
            Gizmos.DrawWireCube(boxCollider2D.transform.position, boxCollider2D.size);
        }

        if (railAnchors.Count == 0) return;

        for (int i = 0; i < railAnchors.Count; i++) {
            RailAnchor2D anchor = railAnchors[i];
            Gizmos.DrawWireSphere(anchor.transform.position, 1);

            if (i+1 == railAnchors.Count) continue;
            RailAnchor2D nextAnchor = railAnchors[i+1];
            Gizmos.DrawLine(anchor.transform.position, nextAnchor.transform.position);    
        }
    }
}
