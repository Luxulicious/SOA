using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomEditor(typeof(ObjectGameEvent))]
    public class ObjectGameEventEditor : GameEventEditor<ObjectGameEvent, ObjectUnityEvent, object>
    {
    }
}