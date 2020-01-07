using SOA.Base;
using UnityEngine;
using System;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New ULong List", menuName = "SOA/Primitives/ULong/List", order = 1)]
    public class ULongReferenceListVariable : ReferenceListVariable<ULongReferenceList, ULongReference, ULongVariable, ulong,
        ULongUnityEvent, ULongULongUnityEvent, ULongReferenceUnityEvent>
    {
    }
}