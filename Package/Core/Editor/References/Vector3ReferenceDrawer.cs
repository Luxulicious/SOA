using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomPropertyDrawer(typeof(Vector3Reference))]
    public class
        Vector3ReferenceDrawer : ReferenceDrawer<Vector3Reference, Vector3Variable, Vector3, Vector3UnityEvent, Vector3Vector3UnityEvent>
    {
    }
}