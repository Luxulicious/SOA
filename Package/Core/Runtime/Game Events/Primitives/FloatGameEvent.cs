using SOA.Base;
using UnityEngine;
using System;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New Float Event", menuName = "SOA/Primitives/Float/Event", order = 1)]
    public class FloatGameEvent : GameEvent<FloatUnityEvent, float>
    {
    }
}