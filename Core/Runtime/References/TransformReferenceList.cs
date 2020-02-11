using System;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class TransformReferenceList : ReferenceList<TransformReference, TransformVariable, Transform, TransformUnityEvent, TransformTransformUnityEvent
        , TransformReferenceUnityEvent>
    {
    }
}