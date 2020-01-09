using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomEditor(typeof(FloatGameEvent))]
    public class FloatGameEventEditor : UnityEventSOEditor<FloatGameEvent, FloatUnityEvent, float>
    {
    }
}