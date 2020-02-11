using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomPropertyDrawer(typeof(Rigidbody2DReference))]
    public class
        Rigidbody2DReferenceDrawer : ReferenceDrawer<Rigidbody2DReference, Rigidbody2DVariable, Rigidbody2D, Rigidbody2DUnityEvent, Rigidbody2DRigidbody2DUnityEvent>
    {
    }
}