using UnityEngine;

[CreateAssetMenu(fileName = "New Agent Profile", menuName = "AI/Agent Profile")]
public class AgentProfile : ScriptableObject
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float acceleration = 8f;
    public float turningDeceleration = 0.8f; 

    [Header("Pathfinding Costs")]
    public int turningPenalty = 5; 
    public bool canMoveDiagonally = true;
}