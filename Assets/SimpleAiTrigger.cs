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

        if (target != null && Vector3.Distance(transform.position, target.position) < 10000)
        {
            Chase();
        }
       
        else if (moveController.IsPathComplete())
        {
            Patrol();
        }
    }

    void Chase()
    {
        
        moveController.RequestPath(target.position, "AStar");
    }

    void Patrol()
    {
        
        if (patrolPoints.Length == 0) return;

        moveController.RequestPath(patrolPoints[patrolIndex].position, "Greedy");
        patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
    }
}