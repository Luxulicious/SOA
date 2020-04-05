using System.Collections.Generic;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.Primitives
{
    [ExecuteAlways]
    public class BoolReferenceComponent : ReferenceComponent<BoolReference, BoolVariable, bool, BoolUnityEvent,
        BoolBoolUnityEvent>
    {
    }
}