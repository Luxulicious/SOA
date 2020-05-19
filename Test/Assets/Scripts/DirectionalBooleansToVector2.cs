using System.Collections;
using System.Collections.Generic;
using SOA.Base;
using SOA.Common.Primitives;
using SOA.Common.UnityEngine;
using UnityEngine;


public class DirectionalBooleansToVector2 : MonoBehaviour
{
    [SerializeField] private BoolReference _up;
    [SerializeField] private BoolReference _down;
    [SerializeField] private BoolReference _right;
    [SerializeField] private BoolReference _left;
    [SerializeField] private Vector2Reference _vector2;

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