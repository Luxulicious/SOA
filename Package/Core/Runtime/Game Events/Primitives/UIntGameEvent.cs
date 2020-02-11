using SOA.Base;
using UnityEngine;
using System;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New UInt Event", menuName = "SOA/Primitives/UInt/Event", order = 1)]
    public class UIntGameEvent : GameEvent<UIntUnityEvent, uint>
    {
    }
}