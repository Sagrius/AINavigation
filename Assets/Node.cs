// --- Node.cs ---
using UnityEngine;

public class Node
{
    public bool isWalkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;

    // Pathfinding data
    public int gCost; // Cost from the starting node
    public int hCost; // Heuristic cost to the target node
    public Node parent; // The node that came before this one in the path

    public int fCost
    {
        get { return gCost + hCost; }
    }

    public Node(bool _isWalkable, Vector3 _worldPosition, int _gridX, int _gridY)
    {
        isWalkable = _isWalkable;
        worldPosition = _worldPosition;
        gridX = _gridX;
        gridY = _gridY;
    }
}