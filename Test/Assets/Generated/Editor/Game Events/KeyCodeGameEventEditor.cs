using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomEditor(typeof(KeyCodeGameEvent))]
    public class KeyCodeGameEventEditor : GameEventEditor<KeyCodeGameEvent, KeyCodeUnityEvent, KeyCode>
    {
    }
}