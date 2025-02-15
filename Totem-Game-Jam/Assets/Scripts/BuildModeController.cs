using UnityEngine;
using UnityEngine.UI;

public class BuildModeController : MonoBehaviour
{

    public bool builderModeActive = true;

    public Sprite go_sprite;
    public Sprite build_sprite;

    [SerializeField] Image builderButton;
    private PlayerBehaviour playerBehaviour;
    private LineController lineController;

    private void Awake()
    {
        playerBehaviour = FindFirstObjectByType<PlayerBehaviour>();
        lineController = FindFirstObjectByType<LineController>();
        EnterBuildMode();
    }



    public void SetBuilderMode(bool newBuilderState)
    {
        if (builderModeActive == newBuilderState) return;
        ToggleBuilderMode();

    }
    public void ToggleBuilderMode()
    {
        builderModeActive = !builderModeActive;
        // If we are now in play mode (after pressing the button)
        if (!builderModeActive)
        {
            EnterPlayMode();
        }
        // If we are now in build mode
        else
        {
            EnterBuildMode();
        }
    }

    private void EnterPlayMode()
    {
        builderButton.sprite = build_sprite;
        playerBehaviour.SetFrozen(false);
        lineController.GenerateCollider(); // Generate line collider
    }

    private void EnterBuildMode()
    {
        builderButton.sprite = go_sprite;
        playerBehaviour.Respawn(true);
    }
}
