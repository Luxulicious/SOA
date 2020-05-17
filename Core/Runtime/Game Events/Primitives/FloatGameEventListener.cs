using System;
using SOA.Base;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class FloatGameEventListener : GameEventListener<FloatGameEvent, FloatUnityEvent, float>
    {
    }
}