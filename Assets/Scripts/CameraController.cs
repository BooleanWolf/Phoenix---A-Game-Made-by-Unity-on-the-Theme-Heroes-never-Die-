using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player
    public Vector3 offset = new Vector3(0, 0, -10); // Default offset for 2D

    void LateUpdate()
    {
        if (player != null)
        {
            // Directly set camera position based on player position and offset
            transform.position = player.position + offset;
        }
    }
}
