using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomPropertyDrawer(typeof(Vector2Reference))]
    public class
        Vector2ReferenceDrawer : ReferenceDrawer<Vector2Reference, Vector2Variable, Vector2, Vector2UnityEvent, Vector2Vector2UnityEvent>
    {
    }
}