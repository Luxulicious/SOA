using SOA.Base;
using UnityEngine;
using System;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New Decimal Variable", menuName = "SOA/Primitives/Decimal/Variable", order = 1)]
    public class DecimalVariable : Variable<decimal, DecimalUnityEvent, DecimalDecimalUnityEvent>
    {
    }
}