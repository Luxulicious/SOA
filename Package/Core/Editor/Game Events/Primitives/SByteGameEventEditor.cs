using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomEditor(typeof(SByteGameEvent))]
    public class SByteGameEventEditor : GameEventEditor<SByteGameEvent, SByteUnityEvent, sbyte>
    {
    }
}