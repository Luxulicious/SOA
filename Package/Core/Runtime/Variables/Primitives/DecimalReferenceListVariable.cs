using SOA.Base;
using UnityEngine;
using System;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New Decimal List", menuName = "SOA/Primitives/Decimal/List", order = 1)]
    public class DecimalReferenceListVariable : ReferenceListVariable<DecimalReferenceList, DecimalReference, DecimalVariable, decimal,
        DecimalUnityEvent, DecimalDecimalUnityEvent, DecimalReferenceUnityEvent>
    {
    }
}