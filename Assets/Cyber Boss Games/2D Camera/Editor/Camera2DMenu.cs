using UnityEditor;
using UnityEngine;

public class CameraMenu2D : MonoBehaviour
{
    [MenuItem("Tools/2D Camera/Rails/Make Rail")]
    static void MakeRail()
    {
        // Create the rail game object
        Rail2D rail = new GameObject("Rail").AddComponent<Rail2D>();
        BoxCollider2D railCollider = rail.GetCollider();
        railCollider.size = new Vector2(20, 15);

        // Add rail anchors
        rail.AddAnchor("Anchor 1", new Vector2(rail.GetCollider().bounds.min.x, 0));
        rail.AddAnchor("Anchor 2", new Vector2(0, 0));
        rail.AddAnchor("Anchor 3", new Vector2(rail.GetCollider().bounds.max.x, 0));

        Selection.activeGameObject = rail.gameObject;
    }

    [MenuItem("Tools/2D Camera/Player/Setup")]
    static void SetupPlayer() {
        
        // Attempt to setup the player object
        // Find the player first
        GameObject playerGo = GameObject.FindGameObjectWithTag("Player");
        if (playerGo == null) {
            // If we don't find the player by tag, try to find it by name
            playerGo = GameObject.Find("Player");
            
            // If we don't find it with this mechanism, throw an error
            if (playerGo == null) {
                Debug.LogError("Failed to find the player! Either add the script 'Player2D' to your player or add the Player Tag to your players GameObject and run this script again!");
                return;
            }
        }

        // Add the player script if it doesn't have it already
        Player2D p2d = playerGo.GetComponent<Player2D>();
        if (p2d != null) {
            Debug.LogWarning("Player already has the Player2D script on it, doing nothing.");
            return;
        }

        playerGo.AddComponent<Player2D>();

        Debug.Log("Player2D script added to player successfully!");
    }

    [MenuItem("Tools/2D Camera/Camera/Setup")]
    static void SetepCamera()
    {
        GameObject cameraGo = new GameObject("2D Camera");
        cameraGo.transform.position = new Vector3(0, 0, -10);
        cameraGo.tag = "MainCamera";

        Camera camera = cameraGo.AddComponent<Camera>();
        camera.orthographic = true;
        camera.orthographicSize = 5;
        camera.depth = -1;
        
        cameraGo.AddComponent<AudioListener>();
        cameraGo.AddComponent<Camera2D>();

        Selection.activeGameObject = cameraGo;
    }
}
