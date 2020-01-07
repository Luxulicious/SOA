using SOA.Base;
using UnityEngine;
using System;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New UShort List", menuName = "SOA/Primitives/UShort/List", order = 1)]
    public class UShortReferenceListVariable : ReferenceListVariable<UShortReferenceList, UShortReference, UShortVariable, ushort,
        UShortUnityEvent, UShortUShortUnityEvent, UShortReferenceUnityEvent>
    {
    }
}