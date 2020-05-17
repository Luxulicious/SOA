using System;
using SOA.Base;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class ObjectGameEventListener : GameEventListener<ObjectGameEvent, ObjectUnityEvent, object>
    {
    }
}