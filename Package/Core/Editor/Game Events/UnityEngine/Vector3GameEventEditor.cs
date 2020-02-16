using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomEditor(typeof(Vector3GameEvent))]
    public class Vector3GameEventEditor : GameEventEditor<Vector3GameEvent, Vector3UnityEvent, Vector3>
    {
    }
}