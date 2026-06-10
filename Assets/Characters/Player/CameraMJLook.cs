using UnityEngine;

public class CameraMJLook : MonoBehaviour
{
    public Transform mjEnemyTransform; // Drag your playerEnemyMJ here

    void LateUpdate()
    {
        // If the static condition is met, warp the camera right to the enemy's face
        if (EnemyKillPlayer.isCameraLockedOnMJ && mjEnemyTransform != null)
        {
            // Detach from the player so we don't move with them anymore
            transform.parent = null;

            // Put the camera position slightly out in front of MJ, looking at him
            transform.position = mjEnemyTransform.position + mjEnemyTransform.forward * 2f + Vector3.up * 1.5f;

            // Lock the eyes onto him
            transform.LookAt(mjEnemyTransform.position + Vector3.up);
        }
    }
}