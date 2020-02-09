using System.Collections;
using System.Collections.Generic;
using SOA.Base;
using SOA.Common.CustomTypes;
using SOA.Common.Primitives;
using UnityEngine;

public class KeyInput : MonoBehaviour
{
    [SerializeField] private KeyCodeReference _keyCode = new KeyCodeReference()
        { Persistence = Persistence.Variable, Scope = Scope.Global };

    [SerializeField] private BoolReference _onKeyPressed = new BoolReference()
        {Persistence = Persistence.Variable, Scope = Scope.Global};

    void Update()
    {
        _onKeyPressed.Value = Input.GetKey(_keyCode.Value);
    }
}