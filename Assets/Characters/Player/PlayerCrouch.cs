using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    public KeyCode crouchKey = KeyCode.LeftControl;

    // Drag your actual main player object (or player visual object) into these slots
    public Transform playerBody;
    public Transform playerCamera;

    void Update()
    {
        // If holding down Control, squash the whole player model down by 50%
        if (Input.GetKey(crouchKey))
        {
            // Vector3(X scale, Y scale, Z scale) -> Y is cut to 0.5 (50%)
            playerBody.localScale = new Vector3(1f, 0.5f, 1f);

            // Push camera down slightly so the player's eyes match the crouch
            playerCamera.localPosition = new Vector3(0f, 0.4f, 0f);
        }
        // If not holding Control, set everything back to full normal size
        else
        {
            // Reset Y scale back to 1 (100% full height)
            playerBody.localScale = new Vector3(1f, 1f, 1f);

            // Reset camera back up to standing eye-level
            playerCamera.localPosition = new Vector3(0f, 0.8f, 0f);
        }
    }
}

// I HAVE NO CLUE WHY THIS DOESN'T WORK STEWPID UNITY LAHFIUABDFIJABDIJOABDJOANF

//                
//        if (Input.GetKey(crouchKey))
//        {
//            bodyCollider.height = crouchHeight;
//            playerCamera.localPosition = new Vector3(0f, crouchHeight, 0f);
//}
//  
//        else
//{
//    bodyCollider.height = standHeight;
//    playerCamera.localPosition = new Vector3(0f, standHeight, 0f);
//}
//    }