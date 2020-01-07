using SOA.Base;
using UnityEngine;
using System;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New Decimal Event", menuName = "SOA/Primitives/Decimal/Event", order = 1)]
    public class DecimalGameEvent : GameEvent<DecimalUnityEvent, decimal>
    {
    }
}