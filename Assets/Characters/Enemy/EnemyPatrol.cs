using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Pathway Design")]
    // pathway drops in dis list
    public Transform[] pathWaypoints;

    private NavMeshAgent agent;
    private int currentPointIndex = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // if points are dere, we start ze walk
        if (pathWaypoints.Length > 0)
        {
            SetDestinationToNextPoint();
        }
    }

    void Update()
    {
        // if no path is set up...do nothing for nopw
        if (pathWaypoints.Length == 0) return;

        // check if granny has reached her current target arrow
        // pathPending means unity is still calculating the route btw
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            SetDestinationToNextPoint();
        }
    }

    void SetDestinationToNextPoint()
    {
        // tell the agent to drive to the current arrow's location...not much else
        agent.destination = pathWaypoints[currentPointIndex].position;

        // choose the next arrow in line
        // the % length trick resets the number to 0 when it hits the end super cool trick
        currentPointIndex = (currentPointIndex + 1) % pathWaypoints.Length;
    }
}