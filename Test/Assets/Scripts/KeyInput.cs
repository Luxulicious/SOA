using SOA.Base;
using SOA.Common.Primitives;
using SOA.Common.UnityEngine;
using UnityEngine;

public class KeyInput : MonoBehaviour
{
    [SerializeField] private KeyCodeReference _keyCode;

    [SerializeField] private BoolReference _onKeyPressed;

    void Update()
    {
        _onKeyPressed.Value = Input.GetKey(_keyCode.Value);
    }
}