using SOA.Base;
using UnityEngine;
using System;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New ULong Event", menuName = "SOA/Primitives/ULong/Event", order = 1)]
    public class ULongGameEvent : GameEvent<ULongUnityEvent, ulong>
    {
    }
}