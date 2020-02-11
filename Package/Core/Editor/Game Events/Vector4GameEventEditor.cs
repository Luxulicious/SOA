using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomEditor(typeof(Vector4GameEvent))]
    public class Vector4GameEventEditor : GameEventEditor<Vector4GameEvent, Vector4UnityEvent, Vector4>
    {
    }
}