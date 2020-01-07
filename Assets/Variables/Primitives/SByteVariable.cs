using SOA.Base;
using UnityEngine;
using System;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New SByte Variable", menuName = "SOA/Primitives/SByte/Variable", order = 1)]
    public class SByteVariable : Variable<sbyte, SByteUnityEvent, SByteSByteUnityEvent>
    {
    }
}