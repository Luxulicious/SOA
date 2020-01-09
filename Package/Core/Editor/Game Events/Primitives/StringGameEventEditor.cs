using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomEditor(typeof(StringGameEvent))]
    public class StringGameEventEditor : UnityEventSOEditor<StringGameEvent, StringUnityEvent, string>
    {
    }
}