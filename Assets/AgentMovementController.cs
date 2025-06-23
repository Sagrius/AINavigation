using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AgentMetrics))]
public class AgentMovementController : MonoBehaviour
{
    public AgentProfile profile;
    public Pathfinder pathfinder; 

    private List<Node> path;
    private int targetIndex;
    private float currentSpeed;
    private AgentMetrics agentMetrics;

    void Awake()
    {
        agentMetrics = GetComponent<AgentMetrics>();
    }

  
    public void RequestPath(Vector3 targetPosition, string algorithm)
    {
        PathResult result = pathfinder.FindPath(transform.position, targetPosition, profile, algorithm);
        if (result.path != null && result.path.Count > 0)
        {
            path = result.path;
            targetIndex = 0;

            
            float distance = 0f;
            for (int i = 0; i < path.Count - 1; i++)
            {
                distance += Vector3.Distance(path[i].worldPosition, path[i + 1].worldPosition);
            }
            agentMetrics.UpdateMetrics(result, distance);

        }
        else
        {
            path = null;
        }
    }

    public bool IsPathComplete()
    {
        return path == null || targetIndex >= path.Count;
    }

    void Update()
    {
        if (!IsPathComplete())
        {
            Vector3 targetPosition = path[targetIndex].worldPosition;
            
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, currentSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                targetIndex++;
                if (IsPathComplete())
                {
                    path = null;
                    currentSpeed = 0;
                }
                else
                {
                    
                    currentSpeed *= profile.turningDeceleration;
                }
            }
        }

        currentSpeed = Mathf.Lerp(currentSpeed, profile.moveSpeed, Time.deltaTime * profile.acceleration);
    }

    void OnDrawGizmos()
    {
        if (path != null)
        {
            Gizmos.color = Color.green;
            for (int i = targetIndex; i < path.Count - 1; i++)
            {
                Gizmos.DrawLine(path[i].worldPosition, path[i + 1].worldPosition);
                Gizmos.DrawSphere(path[i].worldPosition, 0.1f);
            }
            if (path.Count > targetIndex)
            {
                Gizmos.DrawSphere(path[path.Count - 1].worldPosition, 0.1f);
            }
        }
    }
}