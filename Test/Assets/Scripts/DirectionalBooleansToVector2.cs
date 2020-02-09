using System.Collections;
using System.Collections.Generic;
using SOA.Base;
using SOA.Common.Primitives;
using SOA.Common.UnityEngine;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class DirectionalBooleansToVector2 : MonoBehaviour
{
    [SerializeField] private BoolReference _up = new BoolReference(){Persistence = Persistence.Variable, Scope = Scope.Global};
    [SerializeField] private BoolReference _down = new BoolReference() { Persistence = Persistence.Variable, Scope = Scope.Global };
    [SerializeField] private BoolReference _right = new BoolReference() { Persistence = Persistence.Variable, Scope = Scope.Global };
    [SerializeField] private BoolReference _left = new BoolReference() { Persistence = Persistence.Variable, Scope = Scope.Global };
    [SerializeField] private Vector2Reference _vector2 = new Vector2Reference() { Persistence = Persistence.Variable, Scope = Scope.Global };

    void Update()
    {
        UpdateInput();
    }

    private void UpdateInput()
    {
        _vector2.Value = new Vector2((_right.Value ? 1 : 0) - (_left.Value ? 1 : 0),
            (_up.Value ? 1 : 0) - (_down.Value ? 1 : 0));
    }
}