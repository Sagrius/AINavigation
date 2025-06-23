using UnityEngine;

public class SimpleAITrigger : MonoBehaviour
{
    public Transform target; // The target to chase (e.g., the Intruder)
    public Transform[] patrolPoints; // Points for patrolling

    private AgentMovementController moveController;
    private int patrolIndex = 0;

   

    void Start()
    {
        moveController = GetComponent<AgentMovementController>();
        Patrol();
    }

    void Update()
    {
        // Simulate chasing if the target is close
        if (target != null && Vector3.Distance(transform.position, target.position) < 15f)
        {
            Chase();
        }
        // Otherwise, if the path is complete, find the next patrol point
        else if (moveController.IsPathComplete())
        {
            Patrol();
        }
    }

    void Chase()
    {
        // Behavior Graph would transition to "Chase" state and run this logic
        moveController.RequestPath(target.position, "AStar");
    }

    void Patrol()
    {
        // Behavior Graph would be in "Patrol" state
        if (patrolPoints.Length == 0) return;

        moveController.RequestPath(patrolPoints[patrolIndex].position, "Greedy");
        patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
    }
}