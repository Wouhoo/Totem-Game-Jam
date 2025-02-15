using UnityEngine;
using TMPro;

public class MarkerBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _sign;
    [SerializeField] private GameObject _line;
    [SerializeField] private GameObject _arrow;
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
            if (_text.TryGetComponent(out TMPro.TextMeshProUGUI text))
            {
                text.text = _value.ToString();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerBehaviour player))
        {
            if (_type == Type.Upper)
            {
                player.SetUpper(_value);
            }
            else if (_type == Type.Lower)
            {
                player.SetLower(_value);
            }
            else if (_type == Type.Finish)
            {
                player.Finish();
            }
        }
    }
}
