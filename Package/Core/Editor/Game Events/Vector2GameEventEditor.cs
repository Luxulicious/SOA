using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomEditor(typeof(Vector2GameEvent))]
    public class Vector2GameEventEditor : GameEventEditor<Vector2GameEvent, Vector2UnityEvent, Vector2>
    {
    }
}