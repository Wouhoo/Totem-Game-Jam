using UnityEngine;

public class LevelController : MonoBehaviour
{

    readonly float SPEED_UPPER_LIMIT = 1;

    public int levelCompletions = 0;
    public float difficultyFactor = 1;
    GameObject Player;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }


    public void ResetLevelWithIncreasedDifficulty()
    {
        levelCompletions += 1;
        difficultyFactor += 0.1f;


    }

    public void ModifyPlayerSpeed()
    {
        float speedDelta = Random.Range(-0.01f * difficultyFactor, 0.01f * difficultyFactor);
        float currentSpeed = Player.GetComponent<PlayerBehaviour>().Acceleration;

        Player.GetComponent<PlayerBehaviour>().Acceleration = Mathf.Min(Mathf.Max(0, currentSpeed + speedDelta), SPEED_UPPER_LIMIT);
    }
}
