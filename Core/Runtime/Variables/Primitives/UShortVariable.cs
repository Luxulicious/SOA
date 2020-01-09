using SOA.Base;
using UnityEngine;
using System;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New UShort Variable", menuName = "SOA/Primitives/UShort/Variable", order = 1)]
    public class UShortVariable : Variable<ushort, UShortUnityEvent, UShortUShortUnityEvent>
    {
    }
}