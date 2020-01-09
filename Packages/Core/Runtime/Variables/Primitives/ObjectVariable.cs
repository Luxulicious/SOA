using SOA.Base;
using UnityEngine;
using System;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New Object Variable", menuName = "SOA/Primitives/Object/Variable", order = 1)]
    public class ObjectVariable : Variable<object, ObjectUnityEvent, ObjectObjectUnityEvent>
    {
    }
}