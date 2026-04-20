using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform player;       // Player object goes here
    public Vector3 offset;         // Initial distance from player (e.g., 0, 5, -10)
    public float sensitivity = 5f; // Speed of rotation

    private float currentX = 0f;
    private float currentY = 0f;

    void Start()
    {
        // Calculate initial offset if not set in inspector
        if (offset == Vector3.zero) offset = transform.position - player.position;
        
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate() // Use LateUpdate for camera to prevent stuttering
    {
        // Get mouse input
        currentX += Input.GetAxis("Mouse X") * sensitivity;
        currentY -= Input.GetAxis("Mouse Y") * sensitivity;

        // Optional: Clamp vertical rotation to prevent flipping
        currentY = Mathf.Clamp(currentY, -20f, 60f);

        // Apply rotation and update position
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = player.position + rotation * offset;
        transform.LookAt(player.position);
    }
}