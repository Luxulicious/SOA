using SOA.Base;
using UnityEngine;
using System;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New Long Variable", menuName = "SOA/Primitives/Long/Variable", order = 1)]
    public class LongVariable : Variable<long, LongUnityEvent, LongLongUnityEvent>
    {
    }
}