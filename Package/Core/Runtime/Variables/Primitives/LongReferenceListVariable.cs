using SOA.Base;
using UnityEngine;
using System;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New Long List", menuName = "SOA/Primitives/Long/List", order = 1)]
    public class LongReferenceListVariable : ReferenceListVariable<LongReferenceList, LongReference, LongVariable, long,
        LongUnityEvent, LongLongUnityEvent, LongReferenceUnityEvent>
    {
    }
}