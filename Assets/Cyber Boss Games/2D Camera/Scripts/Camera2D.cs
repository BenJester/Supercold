using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Camera2D : MonoBehaviour
{
    /// <summary>
    /// The targets you want the camera to track.
    /// </summary>
    /// <typeparam name="Target2D"></typeparam>
    /// <returns>List of targets</returns>
    public List<Target2D> targets = new List<Target2D>();
    
    /// <summary>
    /// A helper bounding box drawn around all targets to assist the camera in tracking.
    /// </summary>
    private Bounds multiTargetBounds;

    /// <summary>
    /// When there are multiple targets, effect the speed in which the camera zooms.
    /// </summary>
    public float multiTargetAutoZoomDampening = 1.5f;

    /// <summary>
    /// This gives the camera a target to look at. It is manipulated separately than
    /// the targets since it has more flexibility.
    /// </summary>
    private CameraTarget2D cameraTarget;

    /// <summary>
    /// The target offset gives the camera target a position to focus on when idle.
    /// </summary>
    public Vector2 targetOffset;

    /// <summary>
    /// Used when the target offset needs to be reset to its default.
    /// </summary>
    private Vector2 targetOffsetDefault;

    /// <summary>
    /// How fast to track the target along the X axis.
    /// A higher number means a faster track speed
    /// </summary>
    public float followDampeningX = 0.1f;

    /// <summary>
    /// How fast to track the target along the Y axis.
    /// A higher number means a faster track speed
    /// </summary>
    public float followDampeningY = 0.1f;
    
    /// <summary>
    /// The dampening applied when multiple targets are assigned
    /// A higher number means a faster track speed
    /// </summary>
    public float multiTargetDampening = 3.0f;
    
    /// <summary>
    /// The cache camera component.
    /// </summary>
    private Camera cam;

    /// <summary>
    /// A reference velocity to smooth out the camera.
    /// </summary>
    private Vector3 velocity = Vector3.zero;
    
    /// <summary>
    /// The minimum amount the camera can zoom to.
    /// </summary>
    public float minZoom = 1;

    /// <summary>
    /// The maximum amount the camera can zoom to.
    /// </summary>
    public float maxZoom = 10;

    /// <summary>
    /// The celing (sky) height the camera is restricted to.
    /// </summary>
    public float ceilingStopLimit = 11;

    /// <summary>
    /// The floor (ground) height the camera is restricted to.
    /// </summary>
    public float floorStopLimit = -10.5f;

    /// <summary>
    /// The camera looks ahead at this distance facing the same direction
    /// as the player. It gets mutliplied by the targets velocity.
    /// </summary>
    public float lookAheadAmount = 1.0f;

    /// <summary>
    /// Tells us if we are on a rail or not.
    /// </summary>
    private bool isOnRail = false;

    /// <summary>
    /// The target rail if attached.
    /// </summary>
    private Rail2D targetRail;

    void Awake()
    {
        // Get the camera
        cam = GetComponent<Camera>();

        // Create the camera target to follow
        cameraTarget = new GameObject("Camera Target").AddComponent<CameraTarget2D>();

        // Default target
        InitDefaultTarget();

        targetOffsetDefault = targetOffset;

        
    }

    void FixedUpdate()
    {
        targets = new List<Target2D>();
        Target2D target = new Target2D(Player.Instance.currActor.gameObject, true);
        targets.Add(target);
        // If we don't have a target, bail out early
        if (targets.Count == 0) return;

        // Zoom
        if (Input.mouseScrollDelta.y != 0) {
            SetZoom(cam.orthographicSize += Input.mouseScrollDelta.y);
        }

        // If we have multiple targets, auto-zoom to fit
        MutliTargetAutoZoom();

        // Clamp zoom
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
        
        // Stick to the target always
        Vector3 tPos = cameraTarget.transform.position;
        float tY = Mathf.Clamp(tPos.y, floorStopLimit + cam.orthographicSize, ceilingStopLimit - cam.orthographicSize);
        transform.position = new Vector3(tPos.x, tY, transform.position.z);

        // Logic to have the camera target snap to the player
        Look();   

        // Handle rails
        HandleRails();
    }

    /// <summary>
    /// Handle the zooming when multiple targets are set.
    /// </summary>
    private void MutliTargetAutoZoom() {
        if (targets.Count > 1) {
            // Create a bounding box concept around the targets
            multiTargetBounds = targets[0].target.GetComponent<Renderer>().bounds;
            foreach (Target2D target in targets) {
                multiTargetBounds.Encapsulate(target.target.transform.position);
            }

            multiTargetBounds.center = cameraTarget.transform.position;
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, multiTargetBounds.size.x / 2, Time.deltaTime * multiTargetAutoZoomDampening);
        }
    }

    /// <summary>
    /// Attempt to find a default player target.
    /// </summary>
    private void InitDefaultTarget() {
        if (targets.Count == 0) {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) {
                targets.Add(new Target2D(player, true));
            }
        }
    }

    /// <summary>
    /// The main target look loop.
    /// </summary>
    private void Look() {

        // Look ahead is only available for one target (the player or something being controlled typically)
        if (targets.Count == 1) {
            Vector2 lookAhead = Vector2.zero;// targets[0].GetVelocity() * lookAheadAmount;
                Vector3 fromPos = new Vector3(cameraTarget.transform.position.x, cameraTarget.transform.position.y, transform.position.z);
                Vector3 targetPlayerPos = new Vector3(GetTargetPosition(targets[0].target).x + lookAhead.x, GetTargetPosition(targets[0].target).y, transform.position.z);
                cameraTarget.transform.position = new Vector2(Mathf.SmoothDamp(fromPos.x, targetPlayerPos.x, ref velocity.x, 0.1f),
                                                                Mathf.SmoothDamp(fromPos.y, targetPlayerPos.y, ref velocity.y, 0.1f));
        } else if (targets.Count > 1) {
            Vector3 avg = GetMultiTargetVector3Average();
            cameraTarget.transform.position = Vector3.Lerp(cameraTarget.transform.position, avg, Time.deltaTime * multiTargetDampening);
        }
    }

    /// <summary>
    /// Handles rail interaction.
    /// </summary>
    private void HandleRails() {

        if (!isOnRail || targetRail == null) return;

        // Best way seem to be to adjust the y-offset to match the rail path
        Vector3 pos = RailUtils.ProjectPositionOnRail(transform.position, targetRail.railAnchors);
        targetOffset.y = targets[0].target.transform.InverseTransformPoint(pos).y;
    }

    /// <summary>
    /// Helper to set the zoom amount.
    /// </summary>
    /// <param name="zoom"></param>
    public void SetZoom(float zoom) {
        cam.orthographicSize = zoom;
    }

    /// <summary>
    /// Helper to get the targets position. Factors in offset.
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public Vector2 GetTargetPosition(GameObject target) {
        return new Vector2(target.transform.position.x + targetOffset.x, target.transform.position.y + targetOffset.y);
    }

    /// <summary>
    /// Helper to add targets.
    /// </summary>
    /// <param name="targets"></param>
    public void AddTargets(List<Target2D> targets) {
        if (targets == null) return;

        // If the target already exists, don't add it
        targets.ForEach(t => {
            if (!this.targets.Contains(t)) {
                this.targets.Add(t);
            }
        });
    }

    /// <summary>
    /// Clears all targets except the player.
    /// </summary>
    public void ClearAllTargetsExceptPlayer() {
        this.targets.RemoveAll(t => !t.isPlayer);
    }

    /// <summary>
    /// Clears all targets including the player.
    /// </summary>
    public void ClearAllTargets() {
        this.targets.Clear();
    }

    /// <summary>
    /// A position that finds the average between multiple targets.
    /// </summary>
    /// <returns></returns>
    private Vector3 GetMultiTargetVector3Average() {
        Bounds bounds = new Bounds(this.targets[0].target.transform.position, Vector2.one);
        
        foreach (Target2D t in this.targets) {
            bounds.Encapsulate(t.target.transform.position);
        }

        return bounds.center;
    }

    /// <summary>
    /// Helper to set the target rail. Set it to false and null
    /// if you want to clear it.
    /// </summary>
    /// <param name="isOnRail"></param>
    /// <param name="targetRail"></param>
    public void SetIsOnRail(bool isOnRail, Rail2D targetRail) {
        this.isOnRail = isOnRail;
        this.targetRail = targetRail;
    }

    /// <summary>
    /// Resets the target offset back to defaults.
    /// </summary>
    public void ResetOffset() {
        targetOffset = targetOffsetDefault;
    }

    /// <summary>
    /// Draws gizmos.
    /// </summary>
    void OnDrawGizmos()
    {
        if (cameraTarget == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(cameraTarget.transform.position, 1);

        Gizmos.color = Color.red;
        foreach (Target2D target in targets) {
            Gizmos.DrawWireSphere(GetTargetPosition(target.target), 1);
            Gizmos.DrawLine(cameraTarget.transform.position, GetTargetPosition(target.target));
        }

        Gizmos.color = Color.green;
        if (multiTargetBounds != null) {
            Gizmos.DrawWireCube(multiTargetBounds.center, multiTargetBounds.size);
        }
    }
}
