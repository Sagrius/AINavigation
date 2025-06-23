using System.Collections.Generic;

public class PathResult
{
    public List<Node> path;
    public float computationTime;
    public int nodesProcessed;
    public string algorithmUsed;

    public PathResult(List<Node> path, float computationTime, int nodesProcessed, string algorithmUsed)
    {
        this.path = path;
        this.computationTime = computationTime;
        this.nodesProcessed = nodesProcessed;
        this.algorithmUsed = algorithmUsed;
    }
}