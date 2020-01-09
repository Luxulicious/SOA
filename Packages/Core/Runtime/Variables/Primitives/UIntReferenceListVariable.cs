using SOA.Base;
using UnityEngine;
using System;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New UInt List", menuName = "SOA/Primitives/UInt/List", order = 1)]
    public class UIntReferenceListVariable : ReferenceListVariable<UIntReferenceList, UIntReference, UIntVariable, uint,
        UIntUnityEvent, UIntUIntUnityEvent, UIntReferenceUnityEvent>
    {
    }
}