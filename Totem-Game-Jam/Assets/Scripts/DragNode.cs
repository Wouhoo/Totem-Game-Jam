using UnityEngine;

public class DragNode : MonoBehaviour
{
    // Script that allows the player to click & drag nodes at runtime

    [SerializeField] bool freezeX;
    [SerializeField] bool freezeY;
    [SerializeField] Vector2 xBounds;
    [SerializeField] Vector2 yBounds;
    [SerializeField] GameObject[] UIArrows; // Arrows MUST follow the order up > right > down > left!

    private bool isDragging;
    private Vector2 offset;
    private Vector2 startPos;
    private BuildModeController buildModeController;

    private void Start()
    {
        buildModeController = FindFirstObjectByType<BuildModeController>();
        startPos = transform.position;
        // Initially set all UI arrows inactive
        for (int i = 0; i < UIArrows.Length; i++) { UIArrows[i].SetActive(false); }
    }

    private void Update()
    {
        // If dragging the node: update its position
        if (isDragging)
        {
            Vector2 currPos = (Vector2)transform.position;
            Vector2 newPos = GetMousePos() - offset;
            // Keep current X position if X movement is frozen, otherwise update to new X (and likewise for Y)
            float newX = freezeX ? currPos.x : Mathf.Clamp(newPos.x, startPos.x + xBounds[0], startPos.x + xBounds[1]);
            float newY = freezeY ? currPos.y : Mathf.Clamp(newPos.y, startPos.y + yBounds[0], startPos.y + yBounds[1]);
            transform.position = new Vector3(newX, newY, -7);  // Force nodes to always render at same z
        }
    }

    // When clicking on the piece in build mode: start dragging
    private void OnMouseDown()
    {
        if (buildModeController.builderModeActive)
        {
            isDragging = true;
            offset = GetMousePos() - (Vector2)transform.position;
            UISetActive(true);
        }
    }

    // When releasing the piece in build mode: stop dragging
    private void OnMouseUp()
    {
        if (buildModeController.builderModeActive)
        {
            isDragging = false;
            UISetActive(false);
        }
    }

    // Get the mouse's current position
    private Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // Toggle UI arrows
    private void UISetActive(bool active)
    {
        // Toggle left/right arrows if X is not frozen
        if (!freezeX)
        {
            UIArrows[1].SetActive(active);
            UIArrows[3].SetActive(active);
        }
        // Likewise for up/down and Y
        if (!freezeY)
        {
            UIArrows[0].SetActive(active);
            UIArrows[2].SetActive(active);
        }
    }
}
