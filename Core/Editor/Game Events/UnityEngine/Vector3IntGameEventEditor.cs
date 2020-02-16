using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomEditor(typeof(Vector3IntGameEvent))]
    public class Vector3IntGameEventEditor : GameEventEditor<Vector3IntGameEvent, Vector3IntUnityEvent, Vector3Int>
    {
    }
}