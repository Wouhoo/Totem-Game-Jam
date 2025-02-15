using TMPro;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Tooltip("Force applied on the X axis.")]
    public float Acceleration;

    [Space(10)]
    [Tooltip("Whether the Acceleration is applied constantly.")]
    public bool IsConstantlyAccelerating;
    [Tooltip("Limit on X-Axis velocity. If the velocity is surpassed, stop providing further force. Ignored if IsConstantlyAccelerating")]
    public float VelocityLimit;

    public GameObject speedMeter;
    private Rigidbody2D _rigidbody;
    private Vector3 spawnLocation;
    private bool isFrozen = false;

    void Awake()
    {
        _rigidbody = this.GetComponent<Rigidbody2D>();
        spawnLocation = GameObject.FindWithTag("Respawn").transform.position;
    }


    void Update()
    {
        ApplyMovement();
        float currSpeed = _rigidbody.linearVelocity.magnitude;
        speedMeter.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currSpeed.ToString();

        Color speedColor = Color.blue;
        if (currSpeed > 7)
        {
            speedColor = Color.red;
        }
        else if (currSpeed > 4)
        {
            speedColor = Color.yellow;
        }
        speedMeter.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = speedColor;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Hazard") return;

        Kill();
        GameObject.Find("Canvas").GetComponent<BuildModeController>().SetBuilderMode(true);
    }


    private void ApplyMovement()
    {
        if (isFrozen) return;
        if (!IsConstantlyAccelerating && _rigidbody.linearVelocityX >= VelocityLimit) return;

        _rigidbody.AddForce(new Vector2(Acceleration, 0));
    }

    public void SetFrozen(bool newState)
    {
        isFrozen = newState;
        _rigidbody.simulated = !newState;
    }

    public void Respawn(bool asFrozen)
    {
        GetComponent<SpriteRenderer>().enabled = true;
        this.transform.position = spawnLocation;
        _rigidbody.rotation = 0;
        _rigidbody.linearVelocity = new Vector2(0, 0);
        SetFrozen(asFrozen);
    }

    public void Kill()
    {
        SetFrozen(true);
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
