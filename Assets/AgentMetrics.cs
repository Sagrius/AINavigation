// --- AgentMetrics.cs ---
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class AgentMetrics : MonoBehaviour
{
    public float lastPathTime;
    public int lastNodeCount;
    public float lastPathDistance;
    public string lastAlgorithm;
    public int rePathCount = 0;

    public void UpdateMetrics(PathResult result, float distance)
    {
        lastPathTime = result.computationTime;
        lastNodeCount = result.nodesProcessed;
        lastAlgorithm = result.algorithmUsed;
        lastPathDistance = distance;
        rePathCount++;
    }

    void OnDrawGizmosSelected()
    {
#if UNITY_EDITOR
        string metricsText = $"-- Agent Metrics --\n" +
                             $"Algorithm: {lastAlgorithm}\n" +
                             $"Compute Time: {lastPathTime * 1000:F2} ms\n" +
                             $"Nodes Processed: {lastNodeCount}\n" +
                             $"Path Distance: {lastPathDistance:F1}m\n" +
                             $"Re-Paths: {rePathCount}";

        Vector3 textPosition = transform.position + Vector3.up * 2f;
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.white;
        style.fontSize = 12;
        Handles.Label(textPosition, metricsText, style);
#endif
    }
}