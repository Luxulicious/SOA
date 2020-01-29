using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomEditor(typeof(ULongGameEvent))]
    public class ULongGameEventEditor : GameEventEditor<ULongGameEvent, ULongUnityEvent, ulong>
    {
    }
}