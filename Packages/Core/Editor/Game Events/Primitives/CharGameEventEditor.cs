using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomEditor(typeof(CharGameEvent))]
    public class CharGameEventEditor : UnityEventSOEditor<CharGameEvent, CharUnityEvent, char>
    {
    }
}