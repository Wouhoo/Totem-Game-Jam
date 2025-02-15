using UnityEngine;
using UnityEngine.UI;

public class BuildModeController : MonoBehaviour
{

    public bool builderModeActive = true;

    [SerializeField] Image builderButton;
    private PlayerBehaviour playerBehaviour;
    private Color buildModeColor = Color.green;
    private Color playModeColor = Color.red;

    private void Start()
    {
        playerBehaviour = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();
    }

    public void ToggleBuilderMode()
    {
        builderModeActive = !builderModeActive;
        // If we are now in play mode (after pressing the button)
        if (!builderModeActive)
        {
            builderButton.color = playModeColor;
            playerBehaviour.SetFrozen(false);
        }
        // If we are now in build mode
        else
        {
            builderButton.color = buildModeColor;
            playerBehaviour.Respawn(true);
        }
    }
}
