using NUnit.Framework;
using UnityEngine;
using System.Linq;

public class LineController : MonoBehaviour
{
    // Script for drawing a line between the nodes

    private LineRenderer lineRenderer;
    private float lineWidth; // Note: the collider assumes the line's width is constant across all segments
    private Transform[] nodes;
    private PolygonCollider2D polygonCollider;
    private Vector2[] colliderPoints;

    void Start()
    {
        // Get components
        lineRenderer = GetComponent<LineRenderer>();
        lineWidth = lineRenderer.startWidth;
        polygonCollider = GetComponent<PolygonCollider2D>();

        // Get nodes & extract their transforms
        int childCount = transform.childCount;
        nodes = new Transform[childCount];
        for(int i = 0; i < childCount; i++)
        {
            nodes[i] = transform.GetChild(i);
        }
        lineRenderer.positionCount = nodes.Length;
    }

    void LateUpdate()
    {
        // Make line go through nodes
        for (int i = 0; i < nodes.Length; i++) 
        { 
            lineRenderer.SetPosition(i, nodes[i].position);
        }
        // Regenerate collider
        // NOTE: When build & playmode are implemented, this should be done when pressing play, not every frame
        GenerateCollider();
    }

    private void GenerateCollider()
    {
        int no_lines = nodes.Length - 1;
        polygonCollider.pathCount = no_lines;

        // Loop over all lines
        for (int i = 0; i < no_lines; i++)
        {
            Vector2[] endPoints = new Vector2[] {  nodes[i].position, nodes[i+1].position }; // Get endpoints of this line
            Vector2[] colliderPoints = CalculateColliderPoints(endPoints); // Calculate collider points (basically, take line width into account)
            polygonCollider.SetPath(i, colliderPoints); // Add points to collider path
        }
    }

    private Vector2[] CalculateColliderPoints(Vector2[] endPoints)
    {
        // Calculate the corner points of the rectangular line segment between the provided edge points.
        // CONTENT WARNING: MATH
        float slope = (endPoints[1].y - endPoints[0].y) / (endPoints[1].x - endPoints[0].x); // Get slope of the line segment
        float deltaX = (lineWidth / 2f) * (slope / Mathf.Sqrt(slope * slope + 1));           // Get change in X using trigonometry (it just works)
        float deltaY = (lineWidth / 2f) * (1 / Mathf.Sqrt(slope * slope + 1));               // Similar formula for change in Y

        // Calculate offset of actual corner point from line endpoint (which is at the middle of the edge)
        Vector2[] offsets = new Vector2[] { new Vector2(-deltaX, deltaY), new Vector2(deltaX, -deltaY) };

        // Return list of all four corner points, transformed to world position
        Vector2[] colliderPoints = new Vector2[] {
            transform.InverseTransformPoint(endPoints[0] + offsets[0]),
            transform.InverseTransformPoint(endPoints[1] + offsets[0]),
            transform.InverseTransformPoint(endPoints[1] + offsets[1]),
            transform.InverseTransformPoint(endPoints[0] + offsets[1])
        };
        return colliderPoints;
    }
}
