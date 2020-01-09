using SOA.Base;
using UnityEngine;
using System;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New Short Variable", menuName = "SOA/Primitives/Short/Variable", order = 1)]
    public class ShortVariable : Variable<short, ShortUnityEvent, ShortShortUnityEvent>
    {
    }
}