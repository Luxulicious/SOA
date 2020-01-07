using SOA.Base;
using UnityEngine;
using System;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New SByte Event", menuName = "SOA/Primitives/SByte/Event", order = 1)]
    public class SByteGameEvent : GameEvent<SByteUnityEvent, sbyte>
    {
    }
}