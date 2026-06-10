using UnityEngine;
using UnityEngine.AI;

public class proceduralWalker : MonoBehaviour
{
    [Header("IK Targets")]
    public Transform leftFootTarget;
    public Transform rightFootTarget;
    // New glitch variables added right below your original ones
    public Transform glitchLeftTarget;  // Spine
    public Transform glitchRightTarget; // Spine1
    [HideInInspector]
    public bool useGlitchTargets = false;

    [Header("Walk Settings")]
    public float stepDistance = 0.4f; // How far the feet move forward/back
    public float stepHeight = 0.3f;   // How high upward ze lift
    public float walkSpeed = 5f;     // How fast ze cycle through 

    private NavMeshAgent agent;
    private float walkCycle = 0;

    // need these to save the "floor" spot so he doesnt sit in mid air
    private Vector3 leftFootHome;
    private Vector3 rightFootHome;

    void Start()
    {
        // this is to assign to the playerEnemyMJ and something else convinient for future
        agent = GetComponentInParent<NavMeshAgent>();

        // save the original spot u put them in editor so we can move RELATIVE to this
        if (leftFootTarget != null) leftFootHome = leftFootTarget.localPosition;
        if (rightFootTarget != null) rightFootHome = rightFootTarget.localPosition;
    }

    void Update()
    {
        // uhhhh i think its sort of the length of the velocity vector
        float currentSpeed = agent.velocity.magnitude; // uses real speed now

        // only advancethe cycle thing if enemy is actually moving (its set to be above one so charcter
        // isnt running while standing still when against a wall)
        if (currentSpeed > 0.1f)
        {
            walkCycle += Time.deltaTime * currentSpeed * walkSpeed;

            // Left Leg Math (Sine for back/forth, Cosine for up/down) this part was taught by ai
            float lZ = Mathf.Sin(walkCycle) * stepDistance;
            float lY = Mathf.Max(0, Mathf.Cos(walkCycle)) * stepHeight;

            // Right Leg Math (Offset by PI so they move opposite) also taught by ai but i need to learn this by myself still (temp)
            float rZ = Mathf.Sin(walkCycle + Mathf.PI) * stepDistance;
            float rY = Mathf.Max(0, Mathf.Cos(walkCycle + Mathf.PI)) * stepHeight;

            // Decides WHICH targets to manipulate based on the switch
            Transform currentLeft = useGlitchTargets ? glitchLeftTarget : leftFootTarget;
            Transform currentRight = useGlitchTargets ? glitchRightTarget : rightFootTarget;

            // we add the math to the Home position
            // this stops the "teleport to hips" bug because it keeps the original height/width
            if (currentLeft != null) currentLeft.localPosition = leftFootHome + new Vector3(0, lY, lZ);
            if (currentRight != null) currentRight.localPosition = rightFootHome + new Vector3(0, rY, rZ);
        }
    }
}