using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomEditor(typeof(DecimalGameEvent))]
    public class DecimalGameEventEditor : GameEventEditor<DecimalGameEvent, DecimalUnityEvent, decimal>
    {
    }
}