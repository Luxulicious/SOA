using SOA.Base;
using UnityEngine;
using System;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New Float Variable", menuName = "SOA/Primitives/Float/Variable", order = 1)]
    public class FloatVariable : Variable<float ,FloatUnityEvent, FloatFloatUnityEvent>
    {
    }
}