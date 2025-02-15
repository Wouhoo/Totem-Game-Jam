using NUnit.Framework;
using UnityEngine;
using System.Linq;
using System.Collections;
using UnityEngine.SceneManagement;

public class LineController : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private float lineWidth; // Note: the collider assumes the line's width is constant across all segments
    private Transform[] nodes;
    private GameObject[] markers;
    private PolygonCollider2D polygonCollider;
    private Vector2[] colliderPoints;
    private Vector3 zOffset = new Vector3(0, 0, 2); // Make line render behind nodes

    public GameObject winTab;
    bool loadingNextLevel = false;
    float loadingTimer = 0;
    float winDelay = 3.0f; // Number of seconds to wait with loading the level after winning

    void Start()
    {
        // Get components
        lineRenderer = GetComponent<LineRenderer>();
        lineWidth = lineRenderer.startWidth;
        polygonCollider = GetComponent<PolygonCollider2D>();

        // Get nodes & extract their transforms
        int childCount = transform.childCount;
        nodes = new Transform[childCount];
        for (int i = 0; i < childCount; i++)
        {
            nodes[i] = transform.GetChild(i);

        }
        markers = GameObject.FindGameObjectsWithTag("Marker");
        lineRenderer.positionCount = nodes.Length;
        GenerateCollider();
    }

    void LateUpdate()
    {
        // Make line go through nodes
        for (int i = 0; i < nodes.Length; i++) 
        { 
            lineRenderer.SetPosition(i, nodes[i].position + zOffset);
        }
        // Load next level if all markers are touched
        if (loadingNextLevel)
        {
            loadingTimer += Time.deltaTime;
            if (loadingTimer > winDelay)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    // Regenerate collider (called from the build mode controller)
    public void GenerateCollider()
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
            transform.InverseTransformPoint(endPoints[0] + offsets[0]) + zOffset,
            transform.InverseTransformPoint(endPoints[1] + offsets[0]) + zOffset,
            transform.InverseTransformPoint(endPoints[1] + offsets[1]) + zOffset,
            transform.InverseTransformPoint(endPoints[0] + offsets[1]) + zOffset
        };
        return colliderPoints;
    }

    public void NotifyMarkerPassed()
    {
        bool gameComplete = true;
        foreach (GameObject t in markers)
        {
            if (!t.GetComponent<MarkerBehaviour>().hasBeenPassed)
            {
                gameComplete = false;
                break;
            } 
        }

        if (gameComplete)
        {
            winTab.SetActive(true);
            loadingNextLevel = true;
        }
        else print("GAME NOT DONE!");
    }
}
