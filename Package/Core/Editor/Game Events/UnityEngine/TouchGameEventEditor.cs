using SOA.Base;
using SOA.Common.UnityEngine;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomEditor(typeof(TouchGameEvent))]
    public class TouchGameEventEditor : GameEventEditor<TouchGameEvent, TouchUnityEvent, Touch>
    {
    }
}