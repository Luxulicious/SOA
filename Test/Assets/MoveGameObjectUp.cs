using System.Collections;
using System.Collections.Generic;
using SOA.Common.Primitives;
using UnityEngine;

public class MoveGameObjectUp : MonoBehaviour
{
    [SerializeField]
    private GameObjectReferenceComponent _reference;

    void Update()
    {
        _reference.gameObject.transform.position += (Vector3) (Vector2.up * 10f * Time.deltaTime);
    }
}