using SOA.Base;
using UnityEngine;
using System;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New ULong Variable", menuName = "SOA/Primitives/ULong/Variable", order = 1)]
    public class ULongVariable : Variable<ulong, ULongUnityEvent, ULongULongUnityEvent>
    {
    }
}