using System;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class TransformReference : Reference<TransformVariable, Transform, TransformUnityEvent, TransformTransformUnityEvent>
    {
    }
}