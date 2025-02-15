using System;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class DragPowerup : MonoBehaviour
{

    // Script that allows the player to click & drag nodes at runtime

    private bool isDragging;
    private Vector2 offset;
    private BuildModeController buildModeController;
    private bool pickedUp;
    protected GameObject player;

    public void Start()
    {
        buildModeController = FindFirstObjectByType<BuildModeController>();
        player = GameObject.FindWithTag("Player");
    }


    private void Update()
    {
        // If dragging the node: update its position
        if (isDragging)
        {
            Vector2 currPos = (Vector2)transform.position;
            Vector2 newPos = GetMousePos() - offset;
            transform.position = new Vector3(newPos.x, newPos.y, -9); // Force nodes to always render at same 
        }

        else if (buildModeController != null)
        {
            if (isBuilderMode())
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -9);
                this.pickedUp = false;
            } else if (!pickedUp) 
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, player.GetComponent<Rigidbody2D>().transform.position.z);
            }
        }
    }

    // When clicking on the piece in build mode: start dragging
    private void OnMouseDown()
    {
        if (isBuilderMode())
        {
            isDragging = true;
            offset = GetMousePos() - (Vector2)transform.position;
        }
    }

    // When releasing the piece in build mode: stop dragging
    private void OnMouseUp()
    {
        if (isBuilderMode())
        {
            isDragging = false;
        }
    }

    // Get the mouse's current position
    private Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public bool isBuilderMode()
    {
        return buildModeController.builderModeActive;
    }

    public void pickup()
    {
        this.pickedUp = true;
    }
}
