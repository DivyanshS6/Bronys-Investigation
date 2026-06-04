using UnityEngine;

public class moveCamera : MonoBehaviour
{
    public float sensitivity = 2f;
    public Transform orientation;
    float xRotation;
    float yRotation;

    void Update()
    {
        // get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * sensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity;

        yRotation += mouseX;
        xRotation -= mouseY;

        // stop camera from flipping upside down
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // rotate the camera and the orientation compass
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}