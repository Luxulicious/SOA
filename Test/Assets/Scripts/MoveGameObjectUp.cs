using System.Collections;
using System.Collections.Generic;
using SOA.Common.Primitives;
using UnityEngine;

public class MoveGameObjectUp : MonoBehaviour
{
    [SerializeField]
    private GameObjectReferenceComponent _reference;

    public FloatReference speed = new FloatReference(10f);

    void Update()
    {
        _reference.gameObject.transform.position += (Vector3) (Vector2.up * speed.Value * Time.deltaTime);
    }
}