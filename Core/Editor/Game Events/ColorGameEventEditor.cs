using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomEditor(typeof(ColorGameEvent))]
    public class ColorGameEventEditor : UnityEventSOEditor<ColorGameEvent, ColorUnityEvent, Color>
    {
    }
}