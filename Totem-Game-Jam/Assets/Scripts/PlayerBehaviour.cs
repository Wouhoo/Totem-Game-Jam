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

    //public GameObject speedMeter;
    private Rigidbody2D _rigidbody;
    private Vector3 spawnLocation;
    private bool isFrozen = false;

    float timer = 0.1f;
    bool isDead = false;
    void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody2D>();
        spawnLocation = GameObject.FindWithTag("Respawn").transform.position;


        //SetFrozen(true);
    }


    void Update()
    {
        ApplyMovement();
        //speedMeter.GetComponent<TextMeshProUGUI>().text = ""+_rigidbody.linearVelocity.magnitude;
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

    public void Respawn() { Respawn(false); }
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
        transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
