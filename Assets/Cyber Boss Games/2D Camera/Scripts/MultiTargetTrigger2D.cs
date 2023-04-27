using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MultiTargetTrigger2D : Trigger2D
{
    public List<Target2D> targets = new List<Target2D>();

    /// <summary>
    /// Triggered when another Collider2D enters the bounding box.
    /// </summary>
    /// <param name="other"></param>
    public override void Activate(Collider2D other)
    {
        Camera2D cam = GameObject.FindObjectOfType<Camera2D>();
        if (cam == null) return;

        cam.AddTargets(targets);
    }

    /// <summary>
    /// Triggered when another Collider2D leaves the bounding box.
    /// </summary>
    /// <param name="other"></param>
    public override void Deactivate(Collider2D other)
    {
        Camera2D cam = GameObject.FindObjectOfType<Camera2D>();
        if (cam == null) return;

        cam.ClearAllTargetsExceptPlayer();
    }
}
