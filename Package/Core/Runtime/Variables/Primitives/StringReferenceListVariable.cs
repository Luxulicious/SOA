using SOA.Base;
using UnityEngine;
using System;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New String List", menuName = "SOA/Primitives/String/List", order = 1)]
    public class StringReferenceListVariable : ReferenceListVariable<StringReferenceList, StringReference, StringVariable, string,
        StringUnityEvent, StringStringUnityEvent, StringReferenceUnityEvent>
    {
    }
}