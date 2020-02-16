using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomPropertyDrawer(typeof(RigidbodyReference))]
    public class
        RigidbodyReferenceDrawer : ReferenceDrawer<RigidbodyReference, RigidbodyVariable, Rigidbody, RigidbodyUnityEvent, RigidbodyRigidbodyUnityEvent>
    {
    }
}