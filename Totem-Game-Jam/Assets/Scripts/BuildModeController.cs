using UnityEngine;
using UnityEngine.UI;

public class BuildModeController : MonoBehaviour
{

    public bool builderModeActive = true;

    [SerializeField] Image builderButton;
    private PlayerBehaviour playerBehaviour;
    private LineController lineController;
    private Color buildModeColor = Color.green;
    private Color playModeColor = Color.red;

    private void Start()
    {
        playerBehaviour = FindFirstObjectByType<PlayerBehaviour>();
        lineController = FindFirstObjectByType<LineController>();
        EnterBuildMode();
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
        builderButton.color = playModeColor;
        playerBehaviour.SetFrozen(false);
        lineController.GenerateCollider(); // Generate line collider
    }

    private void EnterBuildMode()
    {
        builderButton.color = buildModeColor;
        playerBehaviour.Respawn(true);
    }
}
