using UnityEngine;

public class CameraFollowInitial : MonoBehaviour
{
    public Transform player;       // Assign the player
    public float Camera_Angle_Offset;
    private float initialZ;        // Store initial Z position

    void Start()
    {
        // Save the camera's initial Z value at the start
        initialZ = transform.position.z;
    }

    void LateUpdate()
    {
        // Follow player's X and Y exactly, keep Z fixed
        transform.position = new Vector3(player.position.x, player.position.y+ Camera_Angle_Offset, initialZ );
    }
}