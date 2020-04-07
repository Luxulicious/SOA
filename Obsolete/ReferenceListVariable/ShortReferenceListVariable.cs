using SOA.Base;
using UnityEngine;
using System;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New Short List", menuName = "SOA/Primitives/Short/List", order = 1)]
    public class ShortReferenceListVariable : ReferenceListVariable<ShortReferenceList, ShortReference, ShortVariable, short,
        ShortUnityEvent, ShortShortUnityEvent, ShortReferenceUnityEvent>
    {
    }
}