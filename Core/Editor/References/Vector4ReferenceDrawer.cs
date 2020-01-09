using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomPropertyDrawer(typeof(Vector4Reference))]
    public class
        Vector4ReferenceDrawer : ReferenceDrawer<Vector4Reference, Vector4Variable, Vector4, Vector4UnityEvent, Vector4Vector4UnityEvent>
    {
    }
}