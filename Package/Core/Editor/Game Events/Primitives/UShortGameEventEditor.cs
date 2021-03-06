using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomEditor(typeof(UShortGameEvent))]
    public class UShortGameEventEditor : GameEventEditor<UShortGameEvent, UShortUnityEvent, ushort>
    {
    }
}