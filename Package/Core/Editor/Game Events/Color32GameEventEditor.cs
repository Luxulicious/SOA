using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomEditor(typeof(Color32GameEvent))]
    public class Color32GameEventEditor : UnityEventSOEditor<Color32GameEvent, Color32UnityEvent, Color32>
    {
    }
}