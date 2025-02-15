using UnityEngine;
using TMPro;

public class MarkerBehaviour : MonoBehaviour
{
    //[SerializeField] private GameObject _sign;
    //[SerializeField] private GameObject _line;
    //[SerializeField] private GameObject _arrow;
    [SerializeField] private GameObject _text;

    [SerializeField] private float _value;

    [SerializeField] private SFXPlayer _sfxPlayer;

    public bool hasBeenPassed = false;

    private enum Type
    {
        Upper,
        Lower,
        Finish
    }

    [SerializeField] private Type _type;

    void Awake()
    {
        if (_text.TryGetComponent(out TMPro.TextMeshPro text))
        {
            if (_type != Type.Finish)
            {
                text.text = _value.ToString();
            }
            else
            {
                text.text = "F";
            }
        }
        _sfxPlayer = FindAnyObjectByType<SFXPlayer>();
    }

    void OnTriggerEnter2D(UnityEngine.Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            switch (_type)
            {
                case Type.Upper:
                    {
                        if (collider.gameObject.TryGetComponent(out Rigidbody2D rigidbody))
                        {
                            if (rigidbody.linearVelocity.magnitude > _value)
                            {
                                Debug.Log("Player Over Limit!");
                                _sfxPlayer.FailLevelSound();
                                if (collider.gameObject.TryGetComponent(out PlayerBehaviour player))
                                {
                                    player.Kill();
                                }
                            }
                            else
                            {
                                hasBeenPassed = true;
                                _sfxPlayer.PassSound();
                                GameObject.FindFirstObjectByType<LineController>().NotifyMarkerPassed();

                            }
                        }
                        break;
                    }
                    
                case Type.Lower:
                    {
                        if (collider.gameObject.TryGetComponent(out Rigidbody2D rigidbody))
                        {
                            if (rigidbody.linearVelocity.magnitude < _value)
                            {
                                Debug.Log("Player Under Limit!");
                                _sfxPlayer.FailLevelSound();
                                if (collider.gameObject.TryGetComponent(out PlayerBehaviour player))
                                {
                                    player.Kill();
                                }
                            }
                            else
                            {
                                hasBeenPassed = true;
                                _sfxPlayer.PassSound();
                                FindFirstObjectByType<LineController>().NotifyMarkerPassed();
                            }
                        }
                        break;
                    }
                case Type.Finish:
                    {
                        Debug.Log("Player has finished");
                        break;
                    }
            }
        }
    }
}
