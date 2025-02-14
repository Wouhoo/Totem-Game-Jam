using UnityEngine;

public class DragNode : MonoBehaviour
{
    // Script that allows the player to click & drag nodes at runtime

    [SerializeField] bool freezeX;
    [SerializeField] bool freezeY;
    [SerializeField] GameObject[] UIArrows; // Arrows MUST follow the order up > right > down > left!
    [SerializeField] bool inBuildMode; // For testing purposes this bool can be clicked from the editor
    // Once we have build and playmode implemented, this bool should instead be read from the "state manager"

    private bool isDragging;
    private Vector2 offset;

    private void Start()
    {
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
            transform.position = new Vector2(freezeX ? currPos.x : newPos.x, freezeY ? currPos.y : newPos.y);
        }
    }

    // When clicking on the piece in build mode: start dragging
    private void OnMouseDown()
    {
        if (inBuildMode)
        {
            isDragging = true;
            offset = GetMousePos() - (Vector2)transform.position;
            UISetActive(true);
        }
    }

    // When releasing the piece in build mode: stop dragging
    private void OnMouseUp()
    {
        if (inBuildMode)
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
