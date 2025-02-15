using UnityEngine;
using TMPro;

public class MarkerBehaviour : MonoBehaviour
{
    //[SerializeField] private GameObject _sign;
    //[SerializeField] private GameObject _line;
    //[SerializeField] private GameObject _arrow;
    [SerializeField] private GameObject _text;

    [SerializeField] private float _value;

    private enum Type
    {
        Upper,
        Lower,
        Finish
    }

    [SerializeField] private Type _type;

    void Awake()
    {
        if (_type != Type.Finish)
        {
            if (_text.TryGetComponent(out TMPro.TextMeshPro text))
            {
                text.text = _value.ToString();
            }
        }
    }

    private void OnTriggerEnter(UnityEngine.Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            switch (_type)
            {
                case Type.Upper:
                    {
                        if (collider.TryGetComponent(out Rigidbody2D rigidbody))
                        {
                            if (rigidbody.linearVelocity.magnitude > _value)
                            {
                                Debug.Log("Game Over");
                            }
                            else
                            {
                                Debug.Log("Player has passed");
                            }
                        }
                        break;
                    }
                    
                case Type.Lower:
                    {
                        if (collider.TryGetComponent(out Rigidbody2D rigidbody))
                        {
                            if (rigidbody.linearVelocity.magnitude < _value)
                            {
                                Debug.Log("Game Over");
                            }
                            else
                            {
                                Debug.Log("Player has passed");
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
