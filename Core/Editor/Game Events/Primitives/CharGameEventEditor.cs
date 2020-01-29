using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomEditor(typeof(CharGameEvent))]
    public class CharGameEventEditor : GameEventEditor<CharGameEvent, CharUnityEvent, char>
    {
    }
}