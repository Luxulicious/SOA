using SOA.Base;
using UnityEngine;
using System;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New Long Event", menuName = "SOA/Primitives/Long/Event", order = 1)]
    public class LongGameEvent : GameEvent<LongUnityEvent, long>
    {
    }
}