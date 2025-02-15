using UnityEngine;
using UnityEngine.UI;

public class BuildModeController : MonoBehaviour
{

    public bool builderModeActive = true;

    public GameObject BuilderButton;
    private GameObject Player;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    public void ToggleBuilderMode()
    {
        builderModeActive = !builderModeActive;
        if (builderModeActive)
        {
            BuilderButton.GetComponent<Image>().color = new Color(0, 1, 0);
            Player.GetComponent<PlayerBehaviour>().SetFrozen(false);
        }
        else
        {
            BuilderButton.GetComponent<Image>().color = new Color(1, 0, 0);
            PlayerBehaviour pb = Player.GetComponent<PlayerBehaviour>();
            pb.Respawn(true);
        }

    }



}
