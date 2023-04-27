using UnityEngine;

public class InputHandler : MonoBehaviour {

    private void Awake() {
        // Handle cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}