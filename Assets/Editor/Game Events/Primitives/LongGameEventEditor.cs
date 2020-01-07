using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomEditor(typeof(LongGameEvent))]
    public class LongGameEventEditor : UnityEventSOEditor<LongGameEvent, LongUnityEvent, long>
    {
    }
}