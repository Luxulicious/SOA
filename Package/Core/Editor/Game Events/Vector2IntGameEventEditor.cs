using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomEditor(typeof(Vector2IntGameEvent))]
    public class Vector2IntGameEventEditor : GameEventEditor<Vector2IntGameEvent, Vector2IntUnityEvent, Vector2Int>
    {
    }
}