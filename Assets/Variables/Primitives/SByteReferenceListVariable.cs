using SOA.Base;
using UnityEngine;
using System;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New SByte List", menuName = "SOA/Primitives/SByte/List", order = 1)]
    public class SByteReferenceListVariable : ReferenceListVariable<SByteReferenceList, SByteReference, SByteVariable, sbyte,
        SByteUnityEvent, SByteSByteUnityEvent, SByteReferenceUnityEvent>
    {
    }
}