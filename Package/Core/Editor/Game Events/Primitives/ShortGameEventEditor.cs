using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomEditor(typeof(ShortGameEvent))]
    public class ShortGameEventEditor : GameEventEditor<ShortGameEvent, ShortUnityEvent, short>
    {
    }
}