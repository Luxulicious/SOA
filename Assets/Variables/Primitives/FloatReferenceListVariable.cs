using SOA.Base;
using UnityEngine;
using System;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New Float List", menuName = "SOA/Primitives/Float/List", order = 1)]
    public class FloatReferenceListVariable : ReferenceListVariable<FloatReferenceList, FloatReference, FloatVariable, float,
        FloatUnityEvent, FloatFloatUnityEvent, FloatReferenceUnityEvent>
    {
    }
}