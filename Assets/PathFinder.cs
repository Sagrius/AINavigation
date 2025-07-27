using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics; 

public class Pathfinder : MonoBehaviour
{
    public GridManager gridManager;
    const string GREEDY = "Greedy"; 
    const string ASTAR = "AStar";
    public PathResult FindPath(Vector3 startPos, Vector3 targetPos, AgentProfile profile, string algorithmType)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        Node startNode = gridManager.NodeFromWorldPoint(startPos);
        Node targetNode = gridManager.NodeFromWorldPoint(targetPos);
        int nodesProcessed = 0;

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                bool isLowerFCost = openSet[i].fCost < currentNode.fCost;
                bool isSameFCostButLowerHCost = openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost;

               
                if (algorithmType == GREEDY)
                {
                    if (openSet[i].hCost < currentNode.hCost)
                    {
                        currentNode = openSet[i];
                    }
                }
               

                //This checks for aStar
                else if (algorithmType == ASTAR && isLowerFCost || isSameFCostButLowerHCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);
            nodesProcessed++;

            if (currentNode == targetNode)
            {
                sw.Stop();
                List<Node> path = RetracePath(startNode, targetNode);
                return new PathResult(path, sw.ElapsedMilliseconds / 1000f, nodesProcessed, algorithmType);
            }

            foreach (Node neighbour in gridManager.GetNeighbours(currentNode, profile))
            {
                if (!neighbour.isWalkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int moveCost = currentNode.gCost + GetDistance(currentNode, neighbour);

               
                if (currentNode.parent != null)
                {
                    Vector2 dirOld = new Vector2(currentNode.gridX - currentNode.parent.gridX, currentNode.gridY - currentNode.parent.gridY);
                    Vector2 dirNew = new Vector2(neighbour.gridX - currentNode.gridX, neighbour.gridY - currentNode.gridY);
                    if (dirNew != dirOld)
                    {
                        moveCost += profile.turningPenalty;
                    }
                }

                if (moveCost < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = moveCost;
                    neighbour.hCost = (algorithmType == "Dijkstra") ? 0 : GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }

        sw.Stop();
        return new PathResult(null, sw.ElapsedMilliseconds / 1000f, nodesProcessed, algorithmType);
    }

    private List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;
        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        return path;
    }

    private int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}