using System;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class AnimationCurveGameEventListener : GameEventListener<AnimationCurveGameEvent, AnimationCurveUnityEvent, AnimationCurve>
    {
    }
}