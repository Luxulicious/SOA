using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomPropertyDrawer(typeof(Vector2IntReference))]
    public class
        Vector2IntReferenceDrawer : ReferenceDrawer<Vector2IntReference, Vector2IntVariable, Vector2Int, Vector2IntUnityEvent, Vector2IntVector2IntUnityEvent>
    {
    }
}