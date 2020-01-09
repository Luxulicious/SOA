using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomEditor(typeof(ULongGameEvent))]
    public class ULongGameEventEditor : UnityEventSOEditor<ULongGameEvent, ULongUnityEvent, ulong>
    {
    }
}