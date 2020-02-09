using System.Collections;
using System.Collections.Generic;
using SOA.Base;
using SOA.Common.Primitives;
using SOA.Common.UnityEngine;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public Rigidbody2D _rb;
    public FloatReference _speed = new FloatReference(10f);
    public Vector2Reference _directionalInput;

    void FixedUpdate()
    {
        var velocity = _directionalInput.Value * _speed.Value * Time.fixedDeltaTime;
        _rb.velocity = velocity;
    }
}