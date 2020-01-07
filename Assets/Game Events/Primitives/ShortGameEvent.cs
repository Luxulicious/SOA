using SOA.Base;
using UnityEngine;
using System;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New Short Event", menuName = "SOA/Primitives/Short/Event", order = 1)]
    public class ShortGameEvent : GameEvent<ShortUnityEvent, short>
    {
    }
}