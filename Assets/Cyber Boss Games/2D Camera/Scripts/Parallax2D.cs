using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Parallax2D : MonoBehaviour
{
    public Camera cam;
    public float parallaxEffect;
    private float length;
    private float startPos;
    
    void Awake()
    {
        // Set default camera if it hasn't been assigned in the editor
        if (cam == null) {
            cam = GameObject.FindObjectOfType<Camera>();
        }

        // Initianl Setup
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    /// <summary>
    /// Main loop
    /// </summary>
    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float distance = (cam.transform.position.x * parallaxEffect);
        
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);   
        
        if (temp > startPos + (length/2)) {
            startPos += length;
        } else if (temp < startPos - (length/2)) {
            startPos -= length;
        }
    }
}
