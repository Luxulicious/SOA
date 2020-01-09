using SOA.Base;
using UnityEngine;
using System;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New UInt Variable", menuName = "SOA/Primitives/UInt/Variable", order = 1)]
    public class UIntVariable : Variable<uint, UIntUnityEvent, UIntUIntUnityEvent>
    {
    }
}