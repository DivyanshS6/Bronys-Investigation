using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public Transform orientation;
    public float moveSpeed = 0f; // finally figured out why changing these meant nothing
    public float jumpForce = 0f; // all get over rode in the parameters in the unity editor smh ;-;
    public float airMultiplier = 0.4f; // harder to move while in air

    bool grounded; // is player touching floor?
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // 1. Check for jump input in Update (more responsive)
        if (Input.GetKeyDown(KeyCode.Space) && grounded) // change to getkey instead of getkeydown for 
        {                                                    // for continous jumping when space held down
            Jump();
        }
    }

    void FixedUpdate()
    {
        // 2. Handle movement
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = orientation.forward * z + orientation.right * x;

        // if grounded, move normal. if in air, move slower
        if (grounded)
            rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
        else
            rb.AddForce(moveDir.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
            rb.AddForce(Vector3.down * 20f, ForceMode.Acceleration); // apply stronger grav since game doest use mass * grav, but Vf = Vi + at
        // 3. Check ground (simple raycast down)
        // This shoots a tiny invisible laser down 1.1 units to see if floor is there
        grounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    void Jump()
    {
        // Reset Y velocity so jumping feels consistent
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
}