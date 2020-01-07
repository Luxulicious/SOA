using SOA.Base;
using UnityEngine;
using System;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New Object List", menuName = "SOA/Primitives/Object/List", order = 1)]
    public class ObjectReferenceListVariable : ReferenceListVariable<ObjectReferenceList, ObjectReference, ObjectVariable, object,
        ObjectUnityEvent, ObjectObjectUnityEvent, ObjectReferenceUnityEvent>
    {
    }
}