using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomPropertyDrawer(typeof(Vector3IntReference))]
    public class
        Vector3IntReferenceDrawer : ReferenceDrawer<Vector3IntReference, Vector3IntVariable, Vector3Int, Vector3IntUnityEvent, Vector3IntVector3IntUnityEvent>
    {
    }
}