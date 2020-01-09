using SOA.Base;
using UnityEngine;
using System;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New Char List", menuName = "SOA/Primitives/Char/List", order = 1)]
    public class CharReferenceListVariable : ReferenceListVariable<CharReferenceList, CharReference, CharVariable, char,
        CharUnityEvent, CharCharUnityEvent, CharReferenceUnityEvent>
    {
    }
}