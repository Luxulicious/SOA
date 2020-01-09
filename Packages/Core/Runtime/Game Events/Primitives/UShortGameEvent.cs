using SOA.Base;
using UnityEngine;
using System;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New UShort Event", menuName = "SOA/Primitives/UShort/Event", order = 1)]
    public class UShortGameEvent : GameEvent<UShortUnityEvent, ushort>
    {
    }
}