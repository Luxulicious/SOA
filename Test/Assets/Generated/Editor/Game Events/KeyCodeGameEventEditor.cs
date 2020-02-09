using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.CustomTypes
{
    [CustomEditor(typeof(KeyCodeGameEvent))]
    public class KeyCodeGameEventEditor : GameEventEditor<KeyCodeGameEvent, KeyCodeUnityEvent, KeyCode>
    {
    }
}