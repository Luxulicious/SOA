using SOA.Base;
using UnityEngine;
using System;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New Object Event", menuName = "SOA/Primitives/Object/Event", order = 1)]
    public class ObjectGameEvent : GameEvent<ObjectUnityEvent, object>
    {
    }
}