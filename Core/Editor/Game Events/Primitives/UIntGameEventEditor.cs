using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomEditor(typeof(UIntGameEvent))]
    public class UIntGameEventEditor : UnityEventSOEditor<UIntGameEvent, UIntUnityEvent, uint>
    {
    }
}